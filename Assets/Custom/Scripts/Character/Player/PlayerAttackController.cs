using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour {
	private enum AttackType { Small, Large }

	[Header("Attack Type Variables")]
	[SerializeField] private float smallAttackDuration = 0.2f;
	[SerializeField] private float smallAttackCooldown = 0.5f;
	[SerializeField] private float largeAttackDuration = 0.1f;
	[SerializeField] private float largeAttackCooldown = 0.1f;

	[SerializeField] private AttackType currentAttackType;

	[Header("Hitboxes")]
	[SerializeField] private GameObject smallHitbox;
	[SerializeField] private GameObject largeHitbox;

	[Header("Music Sync Variables")]
	[SerializeField] private float timeThreshold;
	[SerializeField] private MusicEventScriptableObject playerEvent;

	public void Attack() {
		if (playerEvent.CheckEventNearTriggerTime(timeThreshold)) {
			currentAttackType = AttackType.Large;
			largeHitbox.SetActive(true);
		}
		else {
			currentAttackType = AttackType.Small;
			smallHitbox.SetActive(true);
		}
	}

	public void EndAttack() {
		largeHitbox.SetActive(false);
		smallHitbox.SetActive(false);
	}

	public float GetAttackCooldown() {
		return currentAttackType == AttackType.Large ? largeAttackCooldown : smallAttackCooldown;
	}

	public float GetAttackDuration() {
		return currentAttackType == AttackType.Large ? largeAttackDuration : smallAttackDuration;
	}


	private void Start() {
		smallHitbox.SetActive(false);
		largeHitbox.SetActive(false);
	}
}
