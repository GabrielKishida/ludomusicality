using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour {
	[SerializeField] private float smallAttackDuration = 0.2f;
	[SerializeField] private float smallAttackCooldown = 0.5f;

	[SerializeField] private float largeAttackDuration = 0.1f;
	[SerializeField] private float largeAttackCooldown = 0.1f;
	[SerializeField] private MusicEventScriptableObject playerEvent;
	[SerializeField] private GameObject smallHitbox;
	[SerializeField] private GameObject largeHitbox;

	public Vector2 attackDirection = Vector2.zero;

	public bool isAttacking = false;
	public bool isOnCooldown = false;

	public void Attack(Vector2 attackDirection) {
		if (playerEvent.CheckEventTriggerNearTime()) {
			StartCoroutine(LargeAttackCoroutine(attackDirection));
		}
		else {
			StartCoroutine(SmallAttackCoroutine(attackDirection));
		}
	}

	private IEnumerator SmallAttackCoroutine(Vector2 attackDirection) {
		this.attackDirection = attackDirection;
		isAttacking = true;
		smallHitbox.SetActive(true);
		yield return new WaitForSeconds(smallAttackDuration);
		isAttacking = false;
		smallHitbox.SetActive(false);
		StartCoroutine(AttackCooldownCoroutine(smallAttackCooldown));
	}

	private IEnumerator LargeAttackCoroutine(Vector2 attackDirection) {
		this.attackDirection = attackDirection;
		isAttacking = true;
		largeHitbox.SetActive(true);
		yield return new WaitForSeconds(largeAttackDuration);
		isAttacking = false;
		largeHitbox.SetActive(false);
		StartCoroutine(AttackCooldownCoroutine(largeAttackCooldown));
	}

	private IEnumerator AttackCooldownCoroutine(float cooldown) {
		isOnCooldown = true;
		yield return new WaitForSeconds(cooldown);
		isOnCooldown = false;
	}

	private void Start() {
		smallHitbox.SetActive(false);
		largeHitbox.SetActive(false);
	}
}
