using System.Collections;
using UnityEngine;


public class EnemyIdleState : EnemyBaseState {
	private int SelectState() {
		if (ShouldRetreat()) {
			return (int)EnemyState.EnemyRetreatState;
		}
		else if (ShouldAim()) {
			return (int)EnemyState.EnemyAimState;
		}
		else if (ShouldChase()) {
			return (int)EnemyState.EnemyChaseState;
		}
		else {
			return (int)EnemyState.EnemyIdleState;
		}
	}

	public override void Do() {
		nextStateNum = SelectState();
		isComplete = true;
	}
}
