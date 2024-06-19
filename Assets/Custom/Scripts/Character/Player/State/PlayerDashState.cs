using System.Collections;
using UnityEngine;


public class PlayerDashState : PlayerStateBase {
	[SerializeField] private float dashDuration = 0.1f;
	[SerializeField] private float dashCooldown = 1.0f;
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
	}

	public override void Do() {
		movementController.MoveCharacter(dashDirection);
		if (timeSinceStart > dashDuration) {
			isComplete = true;
		}
	}

	public bool IsDashOnCooldown() {
		return timeSinceExit < dashCooldown;
	}
}
