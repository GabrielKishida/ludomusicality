using System.Collections;
using UnityEngine;


public class PlayerHurtState : PlayerStateBase {
	[SerializeField] private float hurtDuration = 0.2f;
	[SerializeField] private float invulnerableDuration = 1.0f;

	public override void Do() {
		movementController.MoveCharacter(inputManager.ReadMovement());
		if (timeSinceStart > hurtDuration) {
			isComplete = true;
		}
	}

	public override void Enter() {
		base.Enter();
		movementController.SetSlowdownSpeed();
	}

	public override void Exit() {
		base.Exit();
		movementController.SetRegularSpeed();
	}

	public bool IsInvulnerable() {
		return timeSinceStart < invulnerableDuration;
	}
}
