using System.Collections;
using UnityEngine;

public class EnemyManager : CharacterManager {
	public EnemyAttackController attackController;

	private IState lookState;
	private IState shootState;

	protected override void Start() {
		base.Start();
		attackController = GetComponent<EnemyAttackController>();

		lookState = new EnemyLookState(this);
		shootState = new EnemyShootState(this);
		stateMachine = new StateMachine(lookState);
	}

	protected override void Update() {
		if (stateMachine.currentState == lookState) {
			if (!attackController.isOnCooldown) {
				stateMachine.TransitionTo(shootState);
			}
		}
		else if (stateMachine.currentState == shootState) {
			stateMachine.TransitionTo(lookState);
		}
		base.Update();
	}
}
