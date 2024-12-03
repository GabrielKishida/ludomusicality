using System.Collections;
using UnityEngine;


public class EnemyChaseState : EnemyBaseState {
	[SerializeField] private float timeBetweenCalculatePath = 0.5f;
	[SerializeField] private float timeSinceLastCalculate = 0;

	public override void Enter() {
		base.Enter();
		movementController.CalculatePathToPlayer();
		timeSinceLastCalculate = 0;
	}

	public override void Do() {
		movementController.RotateTowards(targetTransform.position - transform.position);
		if (movementController.hasPath && !movementController.hasFinishedPath) {
			movementController.MoveAlongPath();
		}
		timeSinceLastCalculate += Time.deltaTime;
		if (timeSinceLastCalculate > timeBetweenCalculatePath) {
			movementController.CalculatePathToPlayer();
			timeSinceLastCalculate = 0;
		}
	}
}
