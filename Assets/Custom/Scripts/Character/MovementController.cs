using System;
using UnityEngine;

public class MovementController : MonoBehaviour {
	[Header("Speed")]
	[SerializeField] protected float verticalSpeed;
	[SerializeField] protected Vector2 horizontalSpeed;

	[Header("Speed Variables")]
	private float regularAccelerationFactor = 1.0f;
	[SerializeField] private float accelerationFactor = 1.0f;
	[SerializeField] private float regularMaxSpeed = 15.0f;
	[SerializeField] public float currentMaxSpeed = 15.0f;
	[SerializeField] public bool shouldRotateOnMovement = true;
	[SerializeField] public float rotationSpeed = 1000f;
	[SerializeField] public float gravity = 5.0f;

	[SerializeField] private float acceleration = 0.5f;
	[SerializeField] private float dragCoefficient = 0.02f;
	[SerializeField] private float minimumSpeed = 0.5f;

	[Header("Slowdown")]
	[SerializeField] private float slowdownMaxSpeed = 5.0f;
	[SerializeField] private float slowdownAccelerationFactor = 0.1f;

	[Header("Dash")]
	[SerializeField] private float dashMaxSpeed = 30.0f;
	[SerializeField] private float dashAccelerationFactor = 10.0f;

	[Header("Slope Handling")]
	[SerializeField] private float slopeHeightLimit = 1.0f;
	private RaycastHit slopeHit;

	[Header("Character Controller")]
	[SerializeField] protected CharacterController controller;

	public bool IsGrounded() {
		return controller.isGrounded || IsOnSlope();
	}

	public void SetRegularSpeed() {
		currentMaxSpeed = regularMaxSpeed;
		accelerationFactor = regularAccelerationFactor;
	}

	public void SetSlowdownSpeed() {
		currentMaxSpeed = slowdownMaxSpeed;
		accelerationFactor = slowdownAccelerationFactor;
	}

	public void SetDashSpeed() {
		currentMaxSpeed = dashMaxSpeed;
		accelerationFactor = dashAccelerationFactor;
	}

	public void ReceiveKnockback(Vector3 knockBack) {
		horizontalSpeed = new Vector2(horizontalSpeed.x + knockBack.x, horizontalSpeed.y + knockBack.z);
		verticalSpeed += knockBack.y;
	}

	public void MoveCharacter(Vector2 moveDirection) {
		if (moveDirection != Vector2.zero) {
			if (shouldRotateOnMovement)
				RotateTowards(new Vector3(moveDirection.x, 0.0f, moveDirection.y));

			Vector3 newHorizontalSpeed = horizontalSpeed + moveDirection.normalized * acceleration * accelerationFactor;
			if (newHorizontalSpeed.magnitude > horizontalSpeed.magnitude) {
				horizontalSpeed = Vector3.ClampMagnitude(newHorizontalSpeed, currentMaxSpeed);
			}
			else {
				horizontalSpeed = newHorizontalSpeed;
			}
		}
	}

	protected bool IsOnSlope() {
		if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, slopeHeightLimit)) {
			float slopeAngle = Vector3.Angle(slopeHit.normal, Vector3.up);
			return slopeAngle < controller.slopeLimit && slopeAngle != 0;
		}
		return false;
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
		if (IsGrounded()) {
			ApplyDrag();
			verticalSpeed = -gravity;
		}
		else {
			verticalSpeed -= gravity;
		}
		Vector3 verticalVelocity = Vector3.up * verticalSpeed;

		Vector3 horizontalVelocity = new Vector3(horizontalSpeed.x, 0, horizontalSpeed.y);
		if (IsOnSlope()) {
			Vector3 adjustedHorizontalVelocity = Vector3.ProjectOnPlane(horizontalVelocity, slopeHit.normal);
			verticalSpeed += adjustedHorizontalVelocity.y;
			controller.Move((adjustedHorizontalVelocity + verticalVelocity) * Time.deltaTime);
		}
		else {
			controller.Move((horizontalVelocity + verticalVelocity) * Time.deltaTime);
		}

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
		currentMaxSpeed = regularMaxSpeed;
	}

	protected virtual void Update() {
		MovementUpdate();
	}
}
