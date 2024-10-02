using UnityEngine;

public enum EnemyState {
	EnemyChaseState,
	EnemyDeathState,
	EnemyHurtState,
	EnemyIdleState,
	EnemyAimState,
	EnemyRetreatState,
	EnemyShootState,
}

public class EnemyBaseState : State {
	protected EnemyMovementController movementController;
	protected EnemyAttackController attackController;
	protected Transform targetTransform;

	public void Setup(EnemyMovementController movementController, EnemyAttackController attackController, Transform targetTransform) {
		this.movementController = movementController;
		this.attackController = attackController;
		this.targetTransform = targetTransform;
		isComplete = false;
	}

	protected bool ShouldRetreat() {
		return attackController.IsOnRetreatDistance() && attackController.HasLineOfSight();
	}

	protected bool ShouldChase() {
		bool hasLosAndShouldAdvance = attackController.HasLineOfSight() && attackController.IsOnAdvanceDistance();
		bool noLosAndNotLost = !attackController.IsOnLoseTargetDistance() && !attackController.HasLineOfSight();
		return hasLosAndShouldAdvance || noLosAndNotLost;
	}

	protected bool ShouldIdle() {
		return attackController.IsOnLoseTargetDistance();
	}

	protected bool ShouldAim() {
		return attackController.HasLineOfSight() && attackController.IsOnAimDistance();
	}
}
