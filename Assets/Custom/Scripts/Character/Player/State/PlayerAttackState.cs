using System.Collections;
using UnityEngine;


public class PlayerAttackState : PlayerStateBase {
	private Vector3 attackDirection;
	public override void Enter() {
		base.Enter();
		Vector2 characterToMouseDirection = inputManager.GetCharacterToMouseDirection();
		attackDirection = new Vector3(characterToMouseDirection.x, 0.0f, characterToMouseDirection.y);
		attackController.Attack();
	}
	public override void Do() {
		movementController.FastRotateTowards(attackDirection);
		if (!IsAttackOccurring()) { isComplete = true; }
	}
	public override void Exit() {
		base.Exit();
		attackController.EndAttack();
	}

	public bool IsAttackOccurring() {
		return timeSinceStart < attackController.GetAttackDuration();
	}
	public bool IsAttackOnCooldown() {
		return timeSinceExit < attackController.GetAttackCooldown();
	}

}
