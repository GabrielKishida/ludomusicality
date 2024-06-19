using UnityEngine;


public class PlayerStateBase : State {
	protected MovementController movementController;
	protected PlayerAttackController attackController;
	protected PlayerInputManager inputManager;
	protected PlayerVisualsController visualsController;

	public void Setup(MovementController movementController, PlayerAttackController attackController, PlayerInputManager inputManager, PlayerVisualsController visualsController) {
		this.movementController = movementController;
		this.attackController = attackController;
		this.inputManager = inputManager;
		this.visualsController = visualsController;
		isComplete = false;
	}
}
