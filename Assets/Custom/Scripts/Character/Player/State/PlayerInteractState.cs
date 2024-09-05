using System.Collections;
using UnityEngine;


public class PlayerInteractState : PlayerStateBase {

	private int SelectState() {
		if (ShouldAttack()) {
			return (int)PlayerState.PlayerHoldAttackState;
		}
		else if (ShouldDash()) {
			return (int)PlayerState.PlayerDashState;
		}
		else if (ShouldMove()) {
			return (int)PlayerState.PlayerMoveState;
		}
		else {
			return (int)PlayerState.PlayerIdleState;
		}
	}

	public override void Enter() {
		interactionController.SetIsInteracting(true);
		base.Enter();
	}

	public override void Exit() {
		interactionController.SetIsInteracting(false);
		base.Enter();
	}

	public override void Do() {
		if (!ShouldKeepInteract() || interactionController.RecentlyFinishedInteraction()) {
			nextStateNum = SelectState();
			isComplete = true;
		}
	}
}
