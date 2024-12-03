using UnityEngine;

public class State : MonoBehaviour {
	public bool isComplete { get; protected set; }

	protected float startTime;
	protected float exitTime;

	public float timeSinceStart => Time.time - startTime;
	public float timeSinceExit => Time.time - exitTime;

	[HideInInspector] public int nextStateNum;

	public virtual void Enter() {
		startTime = Time.time;
		isComplete = false;
	}
	public virtual void Exit() {
		exitTime = Time.time;
	}
	public virtual void Do() { }
	public virtual void FixedDo() { }
}

