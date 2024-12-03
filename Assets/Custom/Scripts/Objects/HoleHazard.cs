using Assets.Custom.Scripts.Objects;
using System.Collections;
using UnityEngine;

public class HoleHazard : MonoBehaviour {
	[SerializeField] private Transform player;

	[SerializeField] private float timeToRespawn = 2.0f;
	[SerializeField] private float hazardDamage = 1.0f;

	IEnumerator OnFallCoroutine() {
		PlayerController.Instance.DisablePlayer();
		yield return new WaitForSeconds(timeToRespawn);
		PlayerController.Instance.TeleportPlayerToSafeSpot();
		PlayerController.Instance.EnablePlayer();
		PlayerController.Instance.LongHurtPlayer(hazardDamage, Vector3.zero);
		yield return null;
	}

	public void OnFallInvoke() {
		StartCoroutine(OnFallCoroutine());
	}
}
