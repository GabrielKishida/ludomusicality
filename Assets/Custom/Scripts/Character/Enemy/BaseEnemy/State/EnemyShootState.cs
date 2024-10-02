using System.Collections;
using UnityEngine;


public class EnemyShootState : EnemyBaseState {
	public override void Enter() {
		base.Enter();
		attackController.Shoot();
	}
	public override void Do() {
		isComplete = true;
	}
}
