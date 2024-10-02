using Assets.Custom.Scripts.Objects;
using System.Collections;
using UnityEngine;

public class HoleHazard : MonoBehaviour {
	[SerializeField] private Transform[] respawnWaypoints;
	[SerializeField] private Transform player;

	[SerializeField] private float timeToRespawn = 2.0f;
	[SerializeField] private float hazardDamage = 1.0f;

	private Vector3 FindNearestRespawnWaypoint() {
		float smallestDistance = float.MaxValue;
		int index = -1;
		for (int i = 0; i < respawnWaypoints.Length; i++) {
			Transform waypoint = respawnWaypoints[i];
			float distance = Vector3.Distance(waypoint.position, player.position);
			if (distance < smallestDistance) {
				index = i;
				smallestDistance = distance;
			}
		}
		return respawnWaypoints[index].position;
	}

	IEnumerator OnFallCoroutine() {
		PlayerController.Instance.DisablePlayer();
		yield return new WaitForSeconds(timeToRespawn);
		PlayerController.Instance.TeleportPlayer(FindNearestRespawnWaypoint());
		PlayerController.Instance.EnablePlayer();
		PlayerController.Instance.LongHurtPlayer(hazardDamage, Vector3.zero);
		yield return null;
	}

	public void OnFallInvoke() {
		StartCoroutine(OnFallCoroutine());
	}
}
