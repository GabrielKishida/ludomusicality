using System;
using UnityEngine;

public class MovementController : MonoBehaviour {
	[Header("Speed")]
	[SerializeField] protected float verticalSpeed;
	[SerializeField] protected Vector2 horizontalSpeed;

	[Header("Speed Variables")]
	protected float regularAccelerationFactor = 1.0f;
	[SerializeField] protected float accelerationFactor = 1.0f;
	[SerializeField] protected float regularMaxSpeed = 15.0f;
	[SerializeField] public float currentMaxSpeed = 15.0f;
	[SerializeField] public bool shouldRotateOnMovement = true;
	[SerializeField] public float rotationSpeed = 1000f;
	[SerializeField] public float gravity = 100.0f;

	[SerializeField] protected float acceleration = 100.0f;
	[SerializeField] protected float dragCoefficient = 0.02f;
	[SerializeField] protected float minimumSpeed = 0.5f;

	[Header("Slowdown")]
	[SerializeField] protected float slowdownMaxSpeed = 5.0f;
	[SerializeField] protected float slowdownAccelerationFactor = 0.1f;

	[Header("Slope Handling")]
	[SerializeField] protected float slopeHeightLimit = 1.0f;
	private RaycastHit slopeHit;

	[Header("Character Controller")]
	[SerializeField] protected CharacterController controller;

	[Header("KnockBack")]
	[SerializeField] protected float knockBackFactor = 0.5f;

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



	public void ReceiveKnockback(Vector3 knockBack) {
		knockBack = knockBackFactor * knockBack;
		horizontalSpeed = new Vector2(horizontalSpeed.x + knockBack.x, horizontalSpeed.y + knockBack.z);
		verticalSpeed += knockBack.y;
	}

	public void MoveCharacter(Vector2 moveDirection) {
		if (moveDirection != Vector2.zero) {
			if (shouldRotateOnMovement)
				RotateTowards(new Vector3(moveDirection.x, 0.0f, moveDirection.y));

			Vector3 newHorizontalSpeed = horizontalSpeed + moveDirection.normalized * acceleration * accelerationFactor * Time.deltaTime;
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
		if (!IsGrounded()) {
			verticalSpeed -= gravity * Time.deltaTime;
		}
		Vector3 verticalVelocity = Vector3.up * verticalSpeed;

		Vector3 horizontalVelocity = new Vector3(horizontalSpeed.x, 0, horizontalSpeed.y);
		if (IsOnSlope()) {
			Vector3 adjustedHorizontalVelocity = Vector3.ProjectOnPlane(horizontalVelocity, slopeHit.normal);
			verticalSpeed += adjustedHorizontalVelocity.y * Time.deltaTime;
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

	public virtual void Start() {
		controller = GetComponent<CharacterController>();
		currentMaxSpeed = regularMaxSpeed;
	}

	public virtual void Update() {
		MovementUpdate();
	}

	public virtual void FixedUpdate() {
		if (IsGrounded()) {
			ApplyDrag();
			verticalSpeed = -0.001f;
		}
	}
}
