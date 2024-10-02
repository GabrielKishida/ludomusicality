using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DashType {
	RegularDash,
	HyperDash,
}

public class PlayerMovementController : MovementController {

	[Header("Grounded Buffer")]
	[SerializeField] private float groundedBufferTime = 0.2f;
	[SerializeField] private float timeSinceGrounded = 0f;

	[Header("Dash")]
	[SerializeField] private float regularDashMaxSpeed = 40.0f;
	[SerializeField] public float hyperDashMaxSpeed = 60.0f;
	[SerializeField] private float dashAccelerationFactor = 20.0f;
	[SerializeField] private float timeSinceDash = 0f;
	[SerializeField] private float dashCooldown = 1.0f;


	[SerializeField] public float dashDuration = 0.1f;

	[SerializeField] public bool isHyperDash = false;

	[Header("Music Sync Variables")]
	[SerializeField] private float timeThreshold;
	[SerializeField] private MusicEventScriptableObject playerEvent;

	public DashType StartDash() {
		timeSinceDash = 0f;

		accelerationFactor = dashAccelerationFactor;
		if (playerEvent.CheckEventNearTriggerTime(timeThreshold)) {
			isHyperDash = true;
			currentMaxSpeed = hyperDashMaxSpeed;
			return DashType.HyperDash;
		}
		else {
			isHyperDash = false;
			currentMaxSpeed = regularDashMaxSpeed;
			return DashType.RegularDash;
		}
	}

	public float GetCurrentDashDuration() {
		return dashDuration;
	}

	public bool IsDashOnCooldown() {
		return timeSinceDash < dashCooldown;
	}

	public bool IsGroundedWithBuffer() {
		return this.IsGrounded() || timeSinceGrounded < groundedBufferTime;
	}

	override public void FixedUpdate() {
		base.FixedUpdate();
		timeSinceDash += Time.deltaTime;
		if (this.IsGrounded()) {
			timeSinceGrounded = 0;
		}
		else {
			timeSinceGrounded += Time.deltaTime;
		}
	}
}
