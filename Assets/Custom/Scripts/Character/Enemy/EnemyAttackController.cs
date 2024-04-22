using System.Collections;
using UnityEngine;
public class EnemyAttackController : MonoBehaviour {
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject projectilePrefab;

	[SerializeField] private float projectileSpeed = 5.0f;
	[SerializeField] private float projectileTimeToLive = 5.0f;

	public Transform target;

	private IEnumerator ShootCoroutine() {
		GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
		Rigidbody rb = projectile.GetComponent<Rigidbody>();
		if (rb != null) {
			rb.velocity = transform.forward * projectileSpeed;
		}
		yield return new WaitForSeconds(projectileTimeToLive);
		Destroy(projectile);
	}

	public void Shoot() {
		StartCoroutine(ShootCoroutine());
	}
}
