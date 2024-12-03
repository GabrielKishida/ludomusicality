using UnityEngine;


public class EnemyDeathState : EnemyBaseState {

	[SerializeField] private Vector3 rotationAxis = new Vector3(1.0f, 1.0f, 1.0f).normalized;

	[SerializeField] private float timeToDespawn = 1.0f;
	private EventScriptableObject whenKilledEvent;

	public void Setup(EnemyMovementController movementController, EnemyAttackController attackController, Transform targetTransform, EventScriptableObject whenKilledEvent) {
		Setup(movementController, attackController, targetTransform);
		this.whenKilledEvent = whenKilledEvent;
	}

	public override void Enter() {
		base.Enter();
		if (whenKilledEvent != null) {
			whenKilledEvent.Invoke();
		}
		Destroy(movementController.gameObject, timeToDespawn);
	}

	public override void Do() {
		movementController.transform.Rotate(rotationAxis * movementController.rotationSpeed * 5.0f * Time.deltaTime);
	}
}
