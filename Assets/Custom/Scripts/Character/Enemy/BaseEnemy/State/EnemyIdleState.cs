using System.Collections;
using UnityEngine;


public class EnemyIdleState : EnemyBaseState {
	public override void Do() {
		nextStateNum = SelectState();
		isComplete = true;
	}
}
