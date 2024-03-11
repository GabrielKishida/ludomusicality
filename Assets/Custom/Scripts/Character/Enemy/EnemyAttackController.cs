using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttackController : MonoBehaviour {
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject projectilePrefab;

	[SerializeField] private float projectileSpeed = 5.0f;
	[SerializeField] private float projectileCooldown = 2.0f;

	public Transform target;
	public bool isOnCooldown = false;

	private IEnumerator ShootCoroutine() {
		isOnCooldown = true;
		GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
		Rigidbody rb = projectile.GetComponent<Rigidbody>();
		if (rb != null) {
			rb.velocity = transform.forward * projectileSpeed;
		}
		yield return new WaitForSeconds(projectileCooldown);
		Destroy(projectile);
		isOnCooldown = false;
	}

	public void Shoot() {
		StartCoroutine(ShootCoroutine());
	}
}
