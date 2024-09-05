using System.Collections;
using UnityEngine;


public class PlayerHoldAttackState : PlayerStateBase {

	public override void Enter() {
		base.Enter();
		movementController.SetSlowdownSpeed();
		movementController.shouldRotateOnMovement = false;
	}

	public override void Do() {
		movementController.MoveCharacter(inputManager.ReadMovement());
		Vector2 characterToMouseDirection = inputManager.GetCharacterToMouseDirection();
		Vector3 attackDirection = new Vector3(characterToMouseDirection.x, 0.0f, characterToMouseDirection.y);
		movementController.RotateTowards(attackDirection);
		if (inputManager.IsAttackReleased()) {
			if (attackController.minAttackHoldTime < timeSinceStart) {
				nextStateNum = (int)PlayerState.PlayerAttackState;
			}
			else {
				nextStateNum = (int)PlayerState.PlayerIdleState;
			}
			isComplete = true;
		}
	}

	public override void Exit() {
		base.Exit();
		movementController.SetRegularSpeed();
		movementController.shouldRotateOnMovement = true;
	}

}
