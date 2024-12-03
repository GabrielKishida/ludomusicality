using System.Collections;
using UnityEngine;


public class EnemyRetreatState : EnemyBaseState {
	[SerializeField] private float retreatDistance = 2.0f;
	[SerializeField] private float timeBetweenCalculatePath = 0.1f;
	[SerializeField] private float timeSinceLastCalculate = 0;

	private Vector3 GetBackwardsDirection() {
		Vector3 toEnemyDirection = targetTransform.position - transform.position;
		return (toEnemyDirection * -1.0f).normalized;
	}

	private void CalculatePath() {
		Vector3 targetPosition = transform.position + GetBackwardsDirection() * retreatDistance;
		movementController.CalculatePathToTransform(targetPosition);
		timeSinceLastCalculate = 0;
	}

	public override void Enter() {
		base.Enter();
		CalculatePath();
	}

	public override void Do() {
		movementController.RotateTowards(targetTransform.position - transform.position);
		if (movementController.hasPath && !movementController.hasFinishedPath) {
			movementController.MoveAlongPath();
		}
		timeSinceLastCalculate += Time.deltaTime;
		if (timeSinceLastCalculate > timeBetweenCalculatePath) {
			CalculatePath();
		}
		nextStateNum = SelectState();
		isComplete = true;
	}
}
