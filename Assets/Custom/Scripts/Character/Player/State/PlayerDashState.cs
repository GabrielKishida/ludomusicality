using System.Collections;
using UnityEngine;


public class PlayerDashState : PlayerStateBase {

	[SerializeField] private Vector2 dashDirection;
	public override void Enter() {
		base.Enter();
		visualsController.SetPlayerColor(Color.gray);
		movementController.SetDashSpeed();
		dashDirection = inputManager.ReadMovement();
	}

	public override void Exit() {
		base.Exit();
		visualsController.ResetPlayerColor();
		movementController.SetRegularSpeed();
		movementController.ResetDashTimer();
	}

	public override void Do() {
		movementController.MoveCharacter(dashDirection);
		if (timeSinceStart > movementController.dashDuration) {
			isComplete = true;
		}
	}
}
