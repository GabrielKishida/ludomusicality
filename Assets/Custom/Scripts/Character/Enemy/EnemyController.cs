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
	[SerializeField] private MovementController movementController;
	[SerializeField] private Hurtbox hurtbox;
	[SerializeField] private Transform targetTransform;

	[Header("State Machine")]
	[SerializeField] private StateMachine stateMachine;

	[SerializeField] private EnemyLookState lookState;
	[SerializeField] private EnemyShootState shootState;
	[SerializeField] private EnemyDeathState deathState;

	private void Start() {
		currentHealth = maxHealth;

		lookState.Setup(movementController, attackController, targetTransform);
		shootState.Setup(movementController, attackController, targetTransform);
		deathState.Setup(movementController, attackController, targetTransform);
		stateMachine.Setup(lookState);

		hurtbox.hurtboxHitEvent.AddListener(OnHurtboxHit);
		enemyAttackEvent.musicEvent.AddListener(ShootEvent);
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
		stateMachine.TransitionTo(lookState);
	}

	private void Update() {
		stateMachine.Do();
		if (stateMachine.currentState.isComplete) {
			SelectState();
		}
	}
}
