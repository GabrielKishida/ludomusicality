using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EnemyState {
	EnemyChaseState,
	EnemyDeathState,
	EnemyHurtState,
	EnemyIdleState,
	EnemyAimState,
	EnemyRetreatState,
	EnemyShootState,
	EnemyPivotState
}

public class EnemyStateProbability {
	public EnemyState state { get; set; }
	public int probability { get; set; } // Probability between 0 and 100
}

public class EnemyBaseState : State {
	protected EnemyMovementController movementController;
	protected EnemyAttackController attackController;
	protected Transform targetTransform;

	List<EnemyStateProbability> losCloseRangeProbabilities = new List<EnemyStateProbability>
{
		new EnemyStateProbability { state = EnemyState.EnemyRetreatState, probability = 70 },
		new EnemyStateProbability { state = EnemyState.EnemyPivotState, probability = 30 },
	};

	List<EnemyStateProbability> noLosCloseRangeProbabilities = new List<EnemyStateProbability>
{
		new EnemyStateProbability { state = EnemyState.EnemyPivotState, probability = 80 },
		new EnemyStateProbability { state = EnemyState.EnemyRetreatState, probability = 20 },
	};

	List<EnemyStateProbability> losShootRangeProbabilities = new List<EnemyStateProbability>
	{
		new EnemyStateProbability { state = EnemyState.EnemyPivotState, probability = 50 },
		new EnemyStateProbability { state = EnemyState.EnemyAimState, probability = 30 },
		new EnemyStateProbability { state = EnemyState.EnemyChaseState, probability = 20 },
	};

	List<EnemyStateProbability> noLosShootRangeProbabilities = new List<EnemyStateProbability>
	{
		new EnemyStateProbability { state = EnemyState.EnemyPivotState, probability = 60 },
		new EnemyStateProbability { state = EnemyState.EnemyChaseState, probability = 30 },
		new EnemyStateProbability { state = EnemyState.EnemyRetreatState, probability = 10 },
	};

	List<EnemyStateProbability> losFarRangeProbabilities = new List<EnemyStateProbability>
	{
		new EnemyStateProbability { state = EnemyState.EnemyChaseState, probability = 50 },
		new EnemyStateProbability { state = EnemyState.EnemyAimState, probability = 20 },
		new EnemyStateProbability { state = EnemyState.EnemyPivotState, probability = 30 },
	};

	List<EnemyStateProbability> noLosFarRangeProbabilities = new List<EnemyStateProbability>
	{
		new EnemyStateProbability { state = EnemyState.EnemyChaseState, probability = 80 },
		new EnemyStateProbability { state = EnemyState.EnemyPivotState, probability = 10 },
		new EnemyStateProbability { state = EnemyState.EnemyAimState, probability = 10 },
	};

	private EnemyState GetRandomState(List<EnemyStateProbability> stateProbabilities) {
		int totalProbability = stateProbabilities.Sum(sp => sp.probability);
		int randomNumber = Random.Range(0, totalProbability);
		int cumulative = 0;
		foreach (var stateProb in stateProbabilities) {
			cumulative += stateProb.probability;
			if (randomNumber < cumulative) {
				return stateProb.state;
			}
		}
		return EnemyState.EnemyIdleState;
	}

	protected int SelectState() {
		if (attackController.HasLineOfSight()) {
			if (attackController.IsOnCloseRange()) {
				return (int)GetRandomState(losCloseRangeProbabilities);
			}
			else if (attackController.IsOnShootrange()) {
				return (int)GetRandomState(losShootRangeProbabilities);
			}
			else if (attackController.IsOnFarRange()) {
				return (int)GetRandomState(losFarRangeProbabilities);
			}
		}
		else {
			if (attackController.IsOnCloseRange()) {
				return (int)GetRandomState(noLosCloseRangeProbabilities);
			}
			else if (attackController.IsOnShootrange()) {
				return (int)GetRandomState(noLosShootRangeProbabilities);
			}
			else if (attackController.IsOnFarRange()) {
				return (int)GetRandomState(noLosFarRangeProbabilities);
			}
		}
		return (int)EnemyState.EnemyIdleState;
	}

	public void Setup(EnemyMovementController movementController, EnemyAttackController attackController, Transform targetTransform) {
		this.movementController = movementController;
		this.attackController = attackController;
		this.targetTransform = targetTransform;
		isComplete = false;
	}

	public void ChangeToNextState() {
		nextStateNum = SelectState();
		isComplete = true;
	}
}
