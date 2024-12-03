using System.Collections;
using UnityEngine;


public class PlayerDashState : PlayerStateBase {

	[SerializeField] private Vector2 dashDirection;
	public override void Enter() {
		base.Enter();
		visualsController.SetCharacterColor(Color.gray);
		movementController.StartDash();
		dashDirection = inputManager.ReadMovement();
	}

	public override void Exit() {
		base.Exit();
		visualsController.ResetCharacterColor();
		movementController.SetRegularSpeed();
	}

	public override void Do() {
		movementController.MoveCharacter(dashDirection);
		if (timeSinceStart > movementController.GetCurrentDashDuration()) {
			isComplete = true;
		}
	}
}
