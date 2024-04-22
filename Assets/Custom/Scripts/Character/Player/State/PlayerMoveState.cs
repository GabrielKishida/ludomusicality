using System.Collections;
using UnityEngine;


public class PlayerMoveState : PlayerStateBase {

	public override void Do() {
		movementController.MoveCharacter(inputManager.ReadMovement());
		isComplete = true;
	}
}
