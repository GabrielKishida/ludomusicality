using System;
using UnityEngine;

[Serializable]
public abstract class PlayerState : IState {
	readonly protected PlayerManager manager;
	public PlayerState(PlayerManager manager) {
		this.manager = manager;
	}
	public abstract void Enter();
	public abstract void Exit();
	public abstract void Update();
}

public class IdleState : PlayerState {
	public IdleState(PlayerManager manager) : base(manager) { }

	public override void Enter() { }
	public override void Exit() { }
	public override void Update() { }
}

public class MoveState : PlayerState {
	public MoveState(PlayerManager manager) : base(manager) { }

	public override void Enter() { }
	public override void Exit() { }
	public override void Update() {
		Vector2 moveInput = manager.move.ReadValue<Vector2>();
		manager.moveController.MoveCharacter(moveInput);
	}
}

public class AttackState : PlayerState {
	public AttackState(PlayerManager manager) : base(manager) { }

	public override void Enter() {
		Vector2 mouseCoordinates = manager.mouseDirection.ReadValue<Vector2>();
		Vector2 characterCoordinates = Camera.main.WorldToScreenPoint(manager.transform.position);
		Vector2 attackDirection = mouseCoordinates - characterCoordinates;
		manager.attackController.Attack(attackDirection);
	}
	public override void Exit() { }
	public override void Update() {
		Vector3 attackDirection = new Vector3(manager.attackController.attackDirection.x, 0.0f, manager.attackController.attackDirection.y);
		manager.moveController.FastRotateTowards(attackDirection);
	}
}

public class HurtState : PlayerState {
	public HurtState(PlayerManager manager) : base(manager) { }

	public override void Enter() { }
	public override void Exit() { }
	public override void Update() { }
}


