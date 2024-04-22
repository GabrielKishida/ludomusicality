using UnityEngine;


public class EnemyStateBase : State {
	protected MovementController movementController;
	protected EnemyAttackController attackController;
	protected Transform targetTransform;

	public void Setup(MovementController movementController, EnemyAttackController attackController, Transform targetTransform) {
		this.movementController = movementController;
		this.attackController = attackController;
		this.targetTransform = targetTransform;
		isComplete = false;
	}
}
