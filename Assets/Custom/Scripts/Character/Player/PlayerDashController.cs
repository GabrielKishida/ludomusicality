using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashController : MonoBehaviour {
	[SerializeField] private float dashCooldown = 1.0f;

	public bool isOnCooldown = false;

	public void Dash() {
		StartCoroutine(DashCoroutine());
	}

	private IEnumerator DashCoroutine() {
		isOnCooldown = true;
		yield return new WaitForSeconds(dashCooldown);
		isOnCooldown = false;
	}
}
