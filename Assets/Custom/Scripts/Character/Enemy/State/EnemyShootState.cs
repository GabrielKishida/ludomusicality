using System.Collections;
using UnityEngine;


public class EnemyShootState : EnemyStateBase {
	public override void Enter() {
		base.Enter();
		attackController.Shoot();
	}
	public override void Do() {
		isComplete = true;
	}
}
