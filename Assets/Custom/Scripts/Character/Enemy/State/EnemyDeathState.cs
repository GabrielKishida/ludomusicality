using UnityEngine;


public class EnemyDeathState : EnemyStateBase {

	[SerializeField] private Vector3 rotationAxis = new Vector3(1.0f, 1.0f, 1.0f).normalized;

	[SerializeField] private float timeToDespawn = 1.0f;
	public override void Enter() {
		base.Enter();
		attackController.Shoot();
		Destroy(movementController.gameObject, timeToDespawn);
	}

	public override void Do() {
		movementController.transform.Rotate(rotationAxis * movementController.rotationSpeed * Time.deltaTime);
	}
}
