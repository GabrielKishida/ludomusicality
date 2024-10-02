using System.Collections;
using UnityEngine;


public class PlayerDashState : PlayerStateBase {

	[SerializeField] private Vector2 dashDirection;
	public override void Enter() {
		base.Enter();
		visualsController.SetPlayerColor(Color.gray);
		DashType dashType = movementController.StartDash();
		dashDirection = inputManager.ReadMovement();
		switch (dashType) {
			case DashType.HyperDash:

				break;
			case DashType.RegularDash:
			default:
				break;
		}
	}

	public override void Exit() {
		base.Exit();
		visualsController.ResetPlayerColor();
		movementController.SetRegularSpeed();
	}

	public override void Do() {
		movementController.MoveCharacter(dashDirection);
		if (timeSinceStart > movementController.GetCurrentDashDuration()) {
			isComplete = true;
		}
	}
}
