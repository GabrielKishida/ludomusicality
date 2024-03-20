using UnityEngine;

public class PlayerState : IState {
	protected PlayerManager manager;
	protected PlayerState(PlayerManager manager) {
		this.manager = manager;
	}
	protected bool ShouldAttack() {
		bool canAttack = !manager.attackController.isAttacking && !manager.attackController.isOnCooldown;
		return canAttack && manager.attack.ReadValue<float>() == 1; ;
	}

	protected bool ShouldMove() {
		return !manager.attackController.isAttacking && manager.move.ReadValue<Vector2>() != Vector2.zero;
	}

	protected bool ShouldIdle() {
		return !manager.attackController.isAttacking && manager.move.ReadValue<Vector2>() == Vector2.zero;
	}

	protected bool ShouldEndAttack() {
		return !manager.attackController.isAttacking;
	}

	override public void Enter() { }
	override public void Update() { }
	override public void Exit() { }
}

public class PlayerIdleState : PlayerState {

	public PlayerIdleState(PlayerManager manager) : base(manager) { }

	public override void Update() {
		if (ShouldAttack()) {
			manager.stateMachine.TransitionTo(manager.attackState);
		}
		else if (ShouldMove()) {
			manager.stateMachine.TransitionTo(manager.moveState);
		}
	}
}

public class PlayerMoveState : PlayerState {

	public PlayerMoveState(PlayerManager manager) : base(manager) { }

	public override void Update() {
		Vector2 moveInput = manager.move.ReadValue<Vector2>();
		manager.moveController.MoveCharacter(moveInput);
		if (ShouldAttack()) { manager.stateMachine.TransitionTo(manager.attackState); }
		if (ShouldIdle()) { manager.stateMachine.TransitionTo(manager.idleState); }
	}
}


public class PlayerAttackState : PlayerState {
	public PlayerAttackState(PlayerManager manager) : base(manager) { }

	public override void Enter() {
		Vector2 mouseCoordinates = manager.mouseDirection.ReadValue<Vector2>();
		Vector2 characterCoordinates = Camera.main.WorldToScreenPoint(manager.transform.position);
		Vector2 attackDirection = mouseCoordinates - characterCoordinates;
		manager.attackController.Attack(attackDirection);
	}
	public override void Update() {
		Vector3 attackDirection = new Vector3(manager.attackController.attackDirection.x, 0.0f, manager.attackController.attackDirection.y);
		manager.moveController.FastRotateTowards(attackDirection);
		if (ShouldEndAttack()) { manager.stateMachine.TransitionTo(manager.idleState); }
	}
}


public class PlayerHurtState : PlayerState {
	public PlayerHurtState(PlayerManager manager, float timeout) : base(manager) {
		timeLimit = timeout;
		timeoutTransitionState = manager.idleState;
	}

	public override void Update() {
		Vector2 moveInput = manager.move.ReadValue<Vector2>();
		manager.moveController.MoveCharacter(moveInput);
	}

	public override void Enter() {
		manager.moveController.SetAccelerationSlow();
	}

	public override void Exit() {
		manager.moveController.SetAccelerationNormal();
	}
}
