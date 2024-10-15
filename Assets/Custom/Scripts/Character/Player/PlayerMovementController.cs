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

	[Header("Safe Spot")]
	[SerializeField] public bool shouldCaptureSafeSpot = true;
	[SerializeField] private float timeSinceSafeSpotUpdate = 0f;
	[SerializeField] private float safeSpotUpdateCooldown = 0.2f;
	[SerializeField] private int safeSpotsMaxCount = 5;
	[SerializeField] private List<Vector3> safeSpots;

	[SerializeField] public float dashDuration = 0.1f;

	[SerializeField] public bool isHyperDash = false;

	[Header("Music Sync Variables")]
	[SerializeField] private float timeThreshold;
	[SerializeField] private MusicEventScriptableObject playerEvent;

	public override void Start() {
		base.Start();
		safeSpots.Insert(0, transform.position);
	}
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

	private void AddSafeSpot(Vector3 safeSpot) {
		safeSpots.Insert(0, safeSpot);
		if (safeSpots.Count > safeSpotsMaxCount) {
			safeSpots.RemoveRange(safeSpotsMaxCount, safeSpots.Count - safeSpotsMaxCount);
		}
	}

	private void UpdateLastSafeSpot() {
		if (shouldCaptureSafeSpot) {
			timeSinceSafeSpotUpdate += Time.deltaTime;
			if (timeSinceSafeSpotUpdate > safeSpotUpdateCooldown) {
				timeSinceSafeSpotUpdate = 0f;
				if (IsGrounded()) {
					AddSafeSpot(transform.position);
				}
			}
		}
	}

	public void TeleportToSafeSpot() {
		if (safeSpots[safeSpotsMaxCount - 1] != null) {
			SetPositionAs(safeSpots[safeSpotsMaxCount - 1]);
		}
		else {
			SetPositionAs(safeSpots[0]);
		}
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
		UpdateLastSafeSpot();
	}
}
