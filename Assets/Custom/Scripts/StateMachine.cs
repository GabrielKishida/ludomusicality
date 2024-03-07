using System;

public delegate void DelegateVoid();

public interface IState {
	void Enter();
	void Exit();
	void Update();

}

[System.Serializable]
public class StateMachine {
	public IState currentState;

	public StateMachine(IState currentState) {
		this.currentState = currentState;
	}

	public void TransitionTo(IState newState) {
		if (currentState == newState) return;
		if (currentState != null) { currentState.Exit(); }
		newState.Enter();
		currentState = newState;
	}

	public void Update() {
		currentState.Update();
	}
}
