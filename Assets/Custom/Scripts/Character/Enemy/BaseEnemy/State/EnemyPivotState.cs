using System.Collections;
using UnityEngine;


public class EnemyPivotState : EnemyBaseState {
	[SerializeField] private float timeBetweenCalculatePath = 0.5f;
	[SerializeField] private float timeSinceLastCalculate = 0;
	[SerializeField] private float pivotDistance = 5f;
	[SerializeField] private float direction = 1;

	private Vector3 GetPerpendicularDirection() {
		Vector3 toEnemyDirection = targetTransform.position - transform.position;
		return Vector3.Cross(toEnemyDirection, Vector3.up).normalized;
	}

	private void CalculatePath() {
		Vector3 targetPosition = transform.position + GetPerpendicularDirection() * pivotDistance * direction;
		bool foundPath = movementController.CalculatePathToTransform(targetPosition);
		if (!foundPath) {
			targetPosition = transform.position + GetPerpendicularDirection() * pivotDistance * -direction;
			movementController.CalculatePathToTransform(targetPosition);
		}
		timeSinceLastCalculate = 0;
	}

	public override void Enter() {
		base.Enter();
		float randomFloat = Random.Range(0, 2);
		direction = randomFloat == 0 ? -1.0f : 1.0f;
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
	}
}
