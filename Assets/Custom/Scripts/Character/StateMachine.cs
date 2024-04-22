using System;
using UnityEngine;

public class StateMachine : MonoBehaviour {
	[SerializeField] public State currentState;
	public void Setup(State initialState) {
		currentState = initialState;
	}

	public void TransitionTo(State newState) {
		if (currentState == newState) return;
		if (currentState != null) { currentState.Exit(); }
		newState.Enter();
		currentState = newState;
	}

	public void Do() {
		currentState.Do();
	}
}
