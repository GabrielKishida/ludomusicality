using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour {


	[Header("Event Scriptable Objects")]
	[SerializeField] private MusicEventScriptableObject enemyAttackEventToListen;
	[SerializeField] private EventScriptableObject activateEventToListen;
	[SerializeField] private EventScriptableObject whenKilledEventToInvoke;

	[Header("Health")]
	[SerializeField] private float maxHealth;
	[SerializeField] private float currentHealth;

	[Header("External Components")]
	[SerializeField] private EnemyAttackController attackController;
	[SerializeField] private EnemyMovementController movementController;
	[SerializeField] private CharacterVisualsController visualsController;
	[SerializeField] private Hurtbox hurtbox;
	[SerializeField] private Transform targetTransform;

	[Header("State Machine")]
	[SerializeField] private bool active = true;
	[SerializeField] private StateMachine stateMachine;
	[SerializeField] private float chanceOfchangingStateOnEvent = 0.3f;

	[SerializeField] private EnemyAimState aimState;
	[SerializeField] private EnemyIdleState idleState;
	[SerializeField] private EnemyRetreatState retreatState;
	[SerializeField] private EnemyHurtState hurtState;
	[SerializeField] private EnemyDeathState deathState;
	[SerializeField] private EnemyChaseState chaseState;
	[SerializeField] private EnemyPivotState pivotState;

	[Header("Hurt")]
	[SerializeField] private float hurtDuration = 1.0f;
	[SerializeField] private bool isHurt = false;
	private Coroutine hurtCoroutine;

	private void Start() {
		currentHealth = maxHealth;

		aimState.Setup(movementController, attackController, targetTransform);
		retreatState.Setup(movementController, attackController, targetTransform);
		idleState.Setup(movementController, attackController, targetTransform);
		chaseState.Setup(movementController, attackController, targetTransform);
		deathState.Setup(movementController, attackController, targetTransform, whenKilledEventToInvoke);
		pivotState.Setup(movementController, attackController, targetTransform);
		stateMachine.Setup(idleState);

		hurtbox.hurtboxHitEvent.AddListener(OnHurtboxHit);
		if (activateEventToListen != null) { activateEventToListen.AddListener(ActivateEnemy); }
		if (enemyAttackEventToListen != null) {
			enemyAttackEventToListen.musicEvent.AddListener(ShootEvent);
			enemyAttackEventToListen.musicEvent.AddListener(ChangeStateEvent);
		}
	}

	private EnemyBaseState GetNextState(int nextStateNum) {
		switch ((EnemyState)nextStateNum) {
			case EnemyState.EnemyChaseState: return chaseState;
			case EnemyState.EnemyDeathState: return deathState;
			case EnemyState.EnemyHurtState: return hurtState;
			case EnemyState.EnemyIdleState: return idleState;
			case EnemyState.EnemyAimState: return aimState;
			case EnemyState.EnemyRetreatState: return retreatState;
			case EnemyState.EnemyPivotState: return pivotState;
			default: return idleState;
		}
	}

	public void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Hazard")) {
			currentHealth = 0;
			stateMachine.TransitionTo(deathState);
		}
	}

	public void ShootEvent() {
		if (currentHealth > 0 && active && !isHurt) { attackController.Shoot(); }
	}

	public void ChangeStateEvent() {
		if (currentHealth > 0 && active) {
			float randomValue = Random.value;
			if (randomValue < chanceOfchangingStateOnEvent) {
				stateMachine.TransitionTo(idleState);
			}
		}
	}

	public void OnHurtboxHit(float damage, Vector3 knockback) {
		currentHealth -= damage;
		if (currentHealth <= 0) {
			stateMachine.TransitionTo(deathState);
		}
		TriggerHurt();
		movementController.ReceiveKnockback(knockback);
	}

	private void TriggerHurt() {
		if (hurtCoroutine != null) {
			StopCoroutine(hurtCoroutine);
		}
		hurtCoroutine = StartCoroutine(HurtCoroutine());
	}

	private IEnumerator HurtCoroutine() {
		visualsController.SetCharacterColor(Color.red);
		isHurt = true;
		movementController.SetSlowdownSpeed();
		yield return new WaitForSeconds(hurtDuration);
		visualsController.ResetCharacterColor();
		movementController.SetRegularSpeed();
		isHurt = false;
		hurtCoroutine = null;
	}

	public void ActivateEnemy() {
		active = true;
	}

	private void SelectState() {
		stateMachine.TransitionTo(GetNextState(stateMachine.currentState.nextStateNum));
	}

	private void FixedUpdate() {
		if (active) {
			stateMachine.Do();
			if (stateMachine.currentState.isComplete) {
				SelectState();
			}
		}
	}
}
