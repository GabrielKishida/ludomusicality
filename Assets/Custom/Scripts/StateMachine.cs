using System;
using UnityEngine;

public delegate void DelegateVoid();

public abstract class IState {
	public float timeLimit = 0;
	public IState timeoutTransitionState;

	public abstract void Enter();
	public abstract void Exit();
	public abstract void Update();
}

[Serializable]
public class StateMachine {
	[SerializeField] public float timeInState = 0;
	[SerializeField] public IState currentState;

	public StateMachine(IState currentState) {
		this.currentState = currentState;
	}

	public void TransitionTo(IState newState) {
		timeInState = 0;
		if (currentState == newState) return;

		if (currentState != null) { currentState.Exit(); }
		newState.Enter();
		currentState = newState;
	}

	public void Update() {
		timeInState += Time.deltaTime;
		if (timeInState > currentState.timeLimit && currentState.timeLimit != 0) {
			if (currentState.timeoutTransitionState != null) {
				TransitionTo(currentState.timeoutTransitionState);
			}
		}
		currentState.Update();
	}
}
