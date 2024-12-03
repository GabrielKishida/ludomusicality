using System.Collections;
using UnityEngine;


public class PlayerAttackState : PlayerStateBase {
	private Vector3 attackDirection;
	public override void Enter() {
		base.Enter();

		attackController.Attack();
	}
	public override void Do() {
		if (!attackController.IsAttackOccurring()) { isComplete = true; }
	}
	public override void Exit() {
		base.Exit();
		attackController.EndAttack();
	}

}
