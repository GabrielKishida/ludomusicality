using UnityEngine;

public enum PlayerState {
	PlayerIdleState,
	PlayerMoveState,
	PlayerDashState,
	PlayerHoldAttackState,
	PlayerAttackState,
	PlayerHurtState,
	PlayerLongHurtState,
	PlayerInteractState,
}
public class PlayerStateBase : State {
	protected PlayerMovementController movementController;
	protected PlayerAttackController attackController;
	protected PlayerInputManager inputManager;
	protected CharacterVisualsController visualsController;
	protected PlayerInteractController interactionController;

	protected bool canPlayerInteract;

	public virtual void Setup(PlayerMovementController movementController, PlayerAttackController attackController, PlayerInputManager inputManager, CharacterVisualsController visualsController, PlayerInteractController interactionController) {
		this.movementController = movementController;
		this.attackController = attackController;
		this.inputManager = inputManager;
		this.visualsController = visualsController;
		this.interactionController = interactionController;
		isComplete = false;
	}

	protected bool ShouldAttack() {
		bool canAttack = !attackController.IsAttackOccurring() && !attackController.IsAttackOnCooldown();
		return canAttack && inputManager.IsAttackPressed();
	}

	protected bool ShouldMove() {
		return !attackController.IsAttackOccurring() && inputManager.ReadMovement() != Vector2.zero;
	}

	protected bool ShouldIdle() {
		return !attackController.IsAttackOccurring() && inputManager.IsMovementZero();
	}

	protected bool ShouldEndAttack() {
		return !attackController.IsAttackOccurring();
	}
	protected bool ShouldDash() {
		return inputManager.WasRollPressed() && !movementController.IsDashOnCooldown() && movementController.IsGroundedWithBuffer();
	}

	protected bool ShouldStartInteract() {
		return inputManager.WasInteractPressed() && interactionController.CanInteract();
	}
	protected bool ShouldKeepInteract() {
		return inputManager.IsInteractPressed() && interactionController.CanInteract();
	}

	public override void Enter() {
		base.Enter();
		nextStateNum = (int)PlayerState.PlayerIdleState;
	}
}
