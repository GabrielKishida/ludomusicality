using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController {

	[Header("Dash")]
	[SerializeField] private float dashMaxSpeed = 40.0f;
	[SerializeField] private float dashAccelerationFactor = 20.0f;
	[SerializeField] private float timeSinceDash = 0f;
	[SerializeField] private float dashCooldown = 1.0f;
	[SerializeField] public float dashDuration = 0.1f;

	public void SetDashSpeed() {
		currentMaxSpeed = dashMaxSpeed;
		accelerationFactor = dashAccelerationFactor;
	}

	public void ResetDashTimer() {
		timeSinceDash = 0f;
	}

	public bool IsDashOnCooldown() {
		return timeSinceDash < dashCooldown;
	}
	override public void FixedUpdate() {
		base.FixedUpdate();
		timeSinceDash += Time.deltaTime;
	}
}
