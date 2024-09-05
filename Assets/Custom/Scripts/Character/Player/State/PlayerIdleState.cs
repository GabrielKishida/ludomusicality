using System.Collections;
using UnityEngine;


public class PlayerIdleState : PlayerStateBase {

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
		else if (ShouldMove()) {
			return (int)PlayerState.PlayerMoveState;
		}
		else {
			return (int)PlayerState.PlayerIdleState;
		}
	}
	public override void Do() {
		nextStateNum = SelectState();
		isComplete = true;
	}
}
