using UnityEngine;

public class CharacterManager : MonoBehaviour {
	public StateMachine stateMachine;
	public MovementController moveController;

	public virtual void OnHurtboxHit(float damage, Vector3 knockback) {
		moveController.ReceiveKnockback(knockback);
	}

	protected virtual void Start() {
		moveController = GetComponent<MovementController>();
	}

	protected virtual void Update() {
		stateMachine.Update();
	}
}
