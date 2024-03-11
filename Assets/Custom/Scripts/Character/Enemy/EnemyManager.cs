using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	[SerializeField] private StateMachine enemyStateMachine;


	public MovementController moveController;
	public EnemyAttackController attackController;


	private IState lookState;
	private IState shootState;

	private void Start() {
		moveController = GetComponent<MovementController>();
		attackController = GetComponent<EnemyAttackController>();

		lookState = new EnemyLookState(this);
		shootState = new EnemyShootState(this);
		enemyStateMachine = new StateMachine(lookState);
	}


	private void Update() {
		if (enemyStateMachine.currentState == lookState) {
			if (!attackController.isOnCooldown) {
				enemyStateMachine.TransitionTo(shootState);
			}
		}
		else if (enemyStateMachine.currentState == shootState) {
			enemyStateMachine.TransitionTo(lookState);
		}
		enemyStateMachine.Update();
	}
}
