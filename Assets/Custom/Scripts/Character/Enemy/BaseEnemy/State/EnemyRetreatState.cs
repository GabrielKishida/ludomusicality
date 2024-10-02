using System.Collections;
using UnityEngine;


public class EnemyRetreatState : EnemyBaseState {
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

	public override void Enter() {
		//movementController.SetSlowdownSpeed();
	}

	public override void Exit() {
		//movementController.SetRegularSpeed();
	}

	public override void Do() {
		movementController.RotateTowards(targetTransform.position - transform.position);
		Vector3 runFromTargetDirection = transform.position - targetTransform.position;
		Vector2 flatDirection = new Vector2(runFromTargetDirection.x, runFromTargetDirection.z).normalized;
		//movementController.MoveCharacter(flatDirection);
		nextStateNum = SelectState();
		isComplete = true;
	}
}
