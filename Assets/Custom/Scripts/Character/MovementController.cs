using Unity.VisualScripting;
using UnityEngine;


public class MovementController : MonoBehaviour {
	[SerializeField] protected float verticalSpeed;
	[SerializeField] protected Vector2 horizontalSpeed;
	[SerializeField] protected float maxSpeed = 15.0f;
	[SerializeField] protected float acceleration = 0.5f;
	[SerializeField] protected float dragCoefficient = 0.02f;
	[SerializeField] protected float minimumSpeed = 0.5f;
	[SerializeField] protected float rotationSpeed = 1000f;
	[SerializeField] protected float gravity = 5.0f;
	[SerializeField] protected float damagedTime = 20.0f;
	[SerializeField] bool shouldRotateOnMovement = true;

	[SerializeField] protected CharacterController controller;

	public void MoveCharacter(Vector2 moveDirection) {
		if (moveDirection != Vector2.zero) {
			if (shouldRotateOnMovement)
				RotateTowards(new Vector3(moveDirection.x, 0.0f, moveDirection.y));

			Vector3 newHorizontalSpeed = horizontalSpeed + moveDirection.normalized * acceleration;
			if (newHorizontalSpeed.magnitude > horizontalSpeed.magnitude) {
				horizontalSpeed = Vector3.ClampMagnitude(newHorizontalSpeed, maxSpeed);
			}
			else {
				horizontalSpeed = newHorizontalSpeed;
			}
		}
	}

	protected void ApplyDrag() {
		if (horizontalSpeed.magnitude < minimumSpeed) {
			horizontalSpeed = Vector2.zero;
		}
		else {
			horizontalSpeed -= horizontalSpeed * dragCoefficient;
		}
	}

	public void MovementUpdate() {
		if (controller.isGrounded) {
			verticalSpeed = 0.0f;
		}
		else {
			verticalSpeed -= gravity;
		}
		ApplyDrag();
		Vector3 velocity = new Vector3(horizontalSpeed.x, verticalSpeed, horizontalSpeed.y);
		controller.Move(velocity * Time.deltaTime);
	}

	public void RotateTowards(Vector3 direction) {
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

	public void FastRotateTowards(Vector3 direction) {
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * 2 * Time.deltaTime);
	}

	protected virtual void Start() {
		controller = GetComponent<CharacterController>();
	}

	protected virtual void Update() {
		MovementUpdate();
	}
}
