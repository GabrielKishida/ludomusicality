using System.Collections;
using UnityEngine;


public class EnemyAimState : EnemyBaseState {

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
		movementController.RotateTowards(targetTransform.position - transform.position);
		nextStateNum = SelectState();
		isComplete = true;
	}
}
