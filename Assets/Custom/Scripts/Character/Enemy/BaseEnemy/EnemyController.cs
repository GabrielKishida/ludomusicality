using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyController : MonoBehaviour {


	[Header("Event Scriptable Objects")]
	[SerializeField] private MusicEventScriptableObject enemyAttackEvent;

	[Header("Health")]
	[SerializeField] private float maxHealth;
	[SerializeField] private float currentHealth;

	[Header("External Components")]
	[SerializeField] private EnemyAttackController attackController;
	[SerializeField] private EnemyMovementController movementController;
	[SerializeField] private Hurtbox hurtbox;
	[SerializeField] private Transform targetTransform;

	[Header("State Machine")]
	[SerializeField] private StateMachine stateMachine;

	[SerializeField] private EnemyAimState aimState;
	[SerializeField] private EnemyIdleState idleState;
	[SerializeField] private EnemyRetreatState retreatState;
	[SerializeField] private EnemyHurtState hurtState;
	[SerializeField] private EnemyShootState shootState;
	[SerializeField] private EnemyDeathState deathState;
	[SerializeField] private EnemyChaseState chaseState;

	private void Start() {
		currentHealth = maxHealth;

		aimState.Setup(movementController, attackController, targetTransform);
		retreatState.Setup(movementController, attackController, targetTransform);
		idleState.Setup(movementController, attackController, targetTransform);
		chaseState.Setup(movementController, attackController, targetTransform);
		shootState.Setup(movementController, attackController, targetTransform);
		deathState.Setup(movementController, attackController, targetTransform);
		stateMachine.Setup(idleState);

		hurtbox.hurtboxHitEvent.AddListener(OnHurtboxHit);
		enemyAttackEvent.musicEvent.AddListener(ShootEvent);
	}

	private EnemyBaseState GetNextState(int nextStateNum) {
		switch ((EnemyState)nextStateNum) {
			case EnemyState.EnemyChaseState: return chaseState;
			case EnemyState.EnemyDeathState: return deathState;
			case EnemyState.EnemyHurtState: return hurtState;
			case EnemyState.EnemyIdleState: return idleState;
			case EnemyState.EnemyAimState: return aimState;
			case EnemyState.EnemyRetreatState: return retreatState;
			case EnemyState.EnemyShootState: return shootState;
			default: return idleState;
		}
	}

	public void ShootEvent() {
		if (currentHealth > 0) { stateMachine.TransitionTo(shootState); }
	}

	public void OnHurtboxHit(float damage, Vector3 knockback) {
		currentHealth -= damage;
		if (currentHealth <= 0) {
			stateMachine.TransitionTo(deathState);
		}
		movementController.ReceiveKnockback(knockback);
	}


	private void SelectState() {
		stateMachine.TransitionTo(GetNextState(stateMachine.currentState.nextStateNum));
	}

	private void FixedUpdate() {
		stateMachine.Do();
		if (stateMachine.currentState.isComplete) {
			SelectState();
		}
	}
}
