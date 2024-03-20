using System;
using UnityEngine;

[Serializable]
public abstract class EnemyState : IState {
	readonly protected EnemyManager manager;
	public EnemyState(EnemyManager manager) {
		this.manager = manager;
	}
}

public class EnemyLookState : EnemyState {
	public EnemyLookState(EnemyManager manager) : base(manager) { }

	public override void Enter() { }
	public override void Exit() { }
	public override void Update() {
		manager.moveController.RotateTowards(manager.attackController.target.transform.position - manager.moveController.transform.position);
		if (!manager.attackController.isOnCooldown) {
			manager.stateMachine.TransitionTo(manager.shootState);
		}
	}
}

public class EnemyShootState : EnemyState {
	public EnemyShootState(EnemyManager manager) : base(manager) { }

	public override void Enter() {
		manager.attackController.Shoot();
	}
	public override void Exit() { }
	public override void Update() { manager.stateMachine.TransitionTo(manager.lookState); }

}

public class EnemyDeathState : EnemyState {
	public EnemyDeathState(EnemyManager manager) : base(manager) { }

	public override void Enter() {
		manager.Die();
	}
	public override void Exit() { }
	public override void Update() {
		Vector3 rotationAxis = new Vector3(1.0f, 1.0f, 1.0f).normalized;
		manager.transform.Rotate(rotationAxis * manager.moveController.rotationSpeed * Time.deltaTime);
	}

}
