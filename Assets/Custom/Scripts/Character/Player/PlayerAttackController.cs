using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour {
	[SerializeField] private float attackDuration = 0.2f;
	[SerializeField] private float attackCooldown = 0.5f;
	[SerializeField] private GameObject hitbox;

	public Vector2 attackDirection = Vector2.zero;

	public bool isAttacking = false;
	public bool isOnCooldown = false;

	public void Attack(Vector2 attackDirection) {
		StartCoroutine(AttackCoroutine(attackDirection));
	}

	private IEnumerator AttackCoroutine(Vector2 attackDirection) {
		this.attackDirection = attackDirection;
		isAttacking = true;
		hitbox.SetActive(true);
		yield return new WaitForSeconds(attackDuration);
		isAttacking = false;
		hitbox.SetActive(false);
		StartCoroutine(AttackCooldownCoroutine());
	}

	private IEnumerator AttackCooldownCoroutine() {
		isOnCooldown = true;
		yield return new WaitForSeconds(attackCooldown);
		isOnCooldown = false;
	}

	private void Start() {
		hitbox.SetActive(false);
	}
}
