using System.Collections;
using UnityEngine;


public class EnemyChaseState : EnemyBaseState {
	[SerializeField] private float timeBetweenCalculatePath = 0.5f;
	[SerializeField] private float timeSinceLastCalculate = 0;

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
		movementController.CalculatePathToTarget();
		timeSinceLastCalculate = 0;
	}

	public override void Do() {
		movementController.RotateTowards(targetTransform.position - transform.position);
		if (movementController.hasPath && !movementController.hasFinishedPath) {
			movementController.MoveAlongPath();
		}
		timeSinceLastCalculate += Time.deltaTime;
		if (timeSinceLastCalculate > timeBetweenCalculatePath) {
			movementController.CalculatePathToTarget();
			timeSinceLastCalculate = 0;
		}
		nextStateNum = SelectState();
		isComplete = true;
	}
}
