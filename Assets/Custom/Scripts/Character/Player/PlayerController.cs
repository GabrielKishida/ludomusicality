using UnityEngine;


public class PlayerController : MonoBehaviour {

	[Header("Event Scriptable Objects")]
	[SerializeField]
	private PlayerHealthEventScriptableObject playerHealth;

	[Header("External Components")]
	[SerializeField] private MovementController movementController;
	[SerializeField] private PlayerAttackController attackController;
	[SerializeField] private PlayerInputManager inputManager;
	[SerializeField] private PlayerVisualsController visualsController;
	[SerializeField] private Hurtbox hurtbox;

	[Header("State Machine")]
	[SerializeField] private StateMachine stateMachine;

	[SerializeField] private PlayerIdleState idleState;
	[SerializeField] private PlayerMoveState moveState;
	[SerializeField] private PlayerAttackState attackState;
	[SerializeField] private PlayerHurtState hurtState;
	[SerializeField] private PlayerDashState dashState;
	protected bool ShouldAttack() {
		bool canAttack = !attackState.IsAttackOccurring() && !attackState.IsAttackOnCooldown();
		return canAttack && inputManager.IsAttackPressed();
	}

	protected bool ShouldMove() {
		return !attackState.IsAttackOccurring() && inputManager.ReadMovement() != Vector2.zero;
	}

	protected bool ShouldIdle() {
		return !attackState.IsAttackOccurring() && inputManager.IsMovementNull();
	}

	protected bool ShouldEndAttack() {
		return !attackState.IsAttackOccurring();
	}
	protected bool ShouldDash() {
		return inputManager.IsRollPressed() && !dashState.IsDashOnCooldown() && movementController.IsGrounded();
	}

	private void SelectState() {
		if (ShouldAttack()) {
			stateMachine.TransitionTo(attackState);
		}
		else if (ShouldDash()) {
			stateMachine.TransitionTo(dashState);
		}
		else if (ShouldMove()) {
			stateMachine.TransitionTo(moveState);
		}
		else {
			stateMachine.TransitionTo(idleState);
		}
	}

	private void Start() {
		idleState.Setup(movementController, attackController, inputManager, visualsController);
		moveState.Setup(movementController, attackController, inputManager, visualsController);
		attackState.Setup(movementController, attackController, inputManager, visualsController);
		hurtState.Setup(movementController, attackController, inputManager, visualsController);
		dashState.Setup(movementController, attackController, inputManager, visualsController);

		stateMachine.Setup(idleState);

		playerHealth.ResetHealth();
		hurtbox.hurtboxHitEvent.AddListener(OnHurtboxHit);
	}

	private void Update() {
		stateMachine.Do();
		if (stateMachine.currentState.isComplete) {
			SelectState();
		}
	}

	private void OnHurtboxHit(float damage, Vector3 knockback) {
		movementController.ReceiveKnockback(knockback);
		if (!hurtState.IsInvulnerable()) {
			if (stateMachine.currentState != hurtState) {
				playerHealth.Hurt(damage);
				stateMachine.TransitionTo(hurtState);
			}
		}
	}

}
