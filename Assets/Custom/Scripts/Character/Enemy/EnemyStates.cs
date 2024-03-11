using System;
using UnityEngine;

[Serializable]
public abstract class EnemyState : IState {
	readonly protected EnemyManager manager;
	public EnemyState(EnemyManager manager) {
		this.manager = manager;
	}
	public abstract void Enter();
	public abstract void Exit();
	public abstract void Update();
}

public class EnemyLookState : EnemyState {
	public EnemyLookState(EnemyManager manager) : base(manager) { }

	public override void Enter() { }
	public override void Exit() { }
	public override void Update() {
		manager.moveController.RotateTowards(manager.attackController.target.transform.position - manager.moveController.transform.position);
	}
}

public class EnemyShootState : EnemyState {
	public EnemyShootState(EnemyManager manager) : base(manager) { }

	public override void Enter() {
		manager.attackController.Shoot();
	}
	public override void Exit() { }
	public override void Update() {

	}
}
