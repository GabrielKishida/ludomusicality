using UnityEngine;


public class PlayerController : MonoBehaviour {

	[Header("Event Scriptable Objects")]
	[SerializeField]
	private PlayerHealthEventScriptableObject playerHealth;

	[Header("External Components")]
	[SerializeField] private PlayerMovementController movementController;
	[SerializeField] private PlayerAttackController attackController;
	[SerializeField] private PlayerInputManager inputManager;
	[SerializeField] private PlayerVisualsController visualsController;
	[SerializeField] private PlayerInteractController interactionController;
	[SerializeField] private Hurtbox hurtbox;

	[Header("State Machine")]
	[SerializeField] private StateMachine stateMachine;


	[SerializeField] private PlayerIdleState idleState;
	[SerializeField] private PlayerMoveState moveState;
	[SerializeField] private PlayerHoldAttackState holdAttackState;
	[SerializeField] private PlayerAttackState attackState;
	[SerializeField] private PlayerHurtState hurtState;
	[SerializeField] private PlayerDashState dashState;
	[SerializeField] private PlayerInteractState interactState;

	private PlayerStateBase GetNextState(int nextStateNum) {
		switch ((PlayerState)nextStateNum) {
			case PlayerState.PlayerIdleState: return idleState;
			case PlayerState.PlayerMoveState: return moveState;
			case PlayerState.PlayerDashState: return dashState;
			case PlayerState.PlayerHoldAttackState: return holdAttackState;
			case PlayerState.PlayerAttackState: return attackState;
			case PlayerState.PlayerHurtState: return hurtState;
			case PlayerState.PlayerInteractState: return interactState;
			default: return idleState;
		}
	}

	private void Start() {
		idleState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		moveState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		holdAttackState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		attackState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		hurtState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		dashState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		interactState.Setup(movementController, attackController, inputManager, visualsController, interactionController);

		stateMachine.Setup(idleState);

		playerHealth.ResetHealth();
		hurtbox.hurtboxHitEvent.AddListener(OnHurtboxHit);
	}

	private void Update() {
		stateMachine.Do();
		if (stateMachine.currentState.isComplete) {
			stateMachine.TransitionTo(GetNextState(stateMachine.currentState.nextStateNum));
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
