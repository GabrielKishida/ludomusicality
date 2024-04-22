using UnityEngine;


public class PlayerStateBase : State {
	protected MovementController movementController;
	protected PlayerAttackController attackController;
	protected PlayerInputManager inputManager;

	public void Setup(MovementController movementController, PlayerAttackController attackController, PlayerInputManager inputManager) {
		this.movementController = movementController;
		this.attackController = attackController;
		this.inputManager = inputManager;
		isComplete = false;
	}
}
