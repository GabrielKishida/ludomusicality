using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour {
	[SerializeField] private PlayerAttackEventScriptableObject attackEventScriptableObject;

	[SerializeField] private float smallAttackDuration = 0.2f;
	[SerializeField] private float smallAttackCooldown = 0.5f;

	[SerializeField] private float largeAttackDuration = 0.1f;
	[SerializeField] private float largeAttackCooldown = 0.1f;
	[SerializeField] private MusicEventScriptableObject playerEvent;
	[SerializeField] private GameObject smallHitbox;
	[SerializeField] private GameObject largeHitbox;

	[SerializeField] private float timeThreshold;

	public void Attack(Vector2 attackDirection) {
		if (playerEvent.CheckEventTriggerNearTime(timeThreshold)) {
			StartCoroutine(LargeAttackCoroutine(attackDirection));
		}
		else {
			StartCoroutine(SmallAttackCoroutine(attackDirection));
		}
	}

	private IEnumerator SmallAttackCoroutine(Vector2 attackDirection) {
		attackEventScriptableObject.isOccurring = true;
		smallHitbox.SetActive(true);
		yield return new WaitForSeconds(smallAttackDuration);
		attackEventScriptableObject.isOccurring = false;
		smallHitbox.SetActive(false);
		StartCoroutine(AttackCooldownCoroutine(smallAttackCooldown));
	}

	private IEnumerator LargeAttackCoroutine(Vector2 attackDirection) {
		attackEventScriptableObject.isOccurring = true;
		largeHitbox.SetActive(true);
		yield return new WaitForSeconds(largeAttackDuration);
		attackEventScriptableObject.isOccurring = false;
		largeHitbox.SetActive(false);
		StartCoroutine(AttackCooldownCoroutine(largeAttackCooldown));
	}

	private IEnumerator AttackCooldownCoroutine(float cooldown) {
		attackEventScriptableObject.isOnCooldown = true;
		yield return new WaitForSeconds(cooldown);
		attackEventScriptableObject.isOnCooldown = false;
	}

	private void Start() {
		smallHitbox.SetActive(false);
		largeHitbox.SetActive(false);
		attackEventScriptableObject.attackEvent.AddListener(Attack);
	}
}
