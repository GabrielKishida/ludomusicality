using System.Collections;
using UnityEngine;


public class PlayerMoveState : PlayerStateBase {
	private int SelectState() {
		if (ShouldAttack()) {
			return (int)PlayerState.PlayerHoldAttackState;
		}
		else if (ShouldDash()) {
			return (int)PlayerState.PlayerDashState;
		}
		else if (ShouldStartInteract()) {
			return (int)PlayerState.PlayerInteractState;
		}
		else {
			return (int)PlayerState.PlayerMoveState;
		}
	}

	public override void Do() {
		Vector2 movement = inputManager.ReadMovement();
		if (movement == Vector2.zero) {
			nextStateNum = (int)PlayerState.PlayerIdleState;
		}
		else {
			nextStateNum = SelectState();
			movementController.MoveCharacter(inputManager.ReadMovement());
		}
		isComplete = true;
	}
}
