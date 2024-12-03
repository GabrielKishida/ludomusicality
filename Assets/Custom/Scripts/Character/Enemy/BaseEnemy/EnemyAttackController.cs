using System.Collections;
using UnityEngine;
public class EnemyAttackController : MonoBehaviour {
	[SerializeField] private Transform firePoint;
	[SerializeField] private GameObject projectilePrefab;

	[SerializeField] private float projectileSpeed = 5.0f;
	[SerializeField] private float projectileTimeToLive = 5.0f;

	[SerializeField] private float targetHeight = 1.0f;
	[SerializeField] private float targetMinDesiredDistance = 7.5f;
	[SerializeField] private float targetMaxDesiredDistance = 10f;
	[SerializeField] private float targetLoseDistance = 30f;

	public LayerMask obstacleLayer;

	public Transform target;

	public bool HasLineOfSight() {
		Vector3 directionToTarget = target.position + new Vector3(0, targetHeight, 0) - firePoint.position;
		float distanceToTarget = directionToTarget.magnitude;
		return !Physics.Raycast(transform.position, directionToTarget.normalized, out RaycastHit hit, distanceToTarget, obstacleLayer);
	}

	public bool IsOnCloseRange() {
		float distance = Vector3.Distance(target.position, firePoint.position);
		return distance < targetMinDesiredDistance;
	}

	public bool IsOnFarRange() {
		float distance = Vector3.Distance(target.position, firePoint.position);
		return distance > targetMaxDesiredDistance;
	}

	public bool IsOnShootrange() {
		float distance = Vector3.Distance(target.position, firePoint.position);
		return targetMinDesiredDistance < distance && distance < targetMaxDesiredDistance;
	}

	public bool IsOnLoseTargetDistance() {
		float distance = Vector3.Distance(target.position, firePoint.position);
		return distance > targetLoseDistance;
	}

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
	private void OnDrawGizmos() {
		if (target != null) {
			Gizmos.color = HasLineOfSight() ? Color.green : Color.red;
			Gizmos.DrawLine(firePoint.position, target.position + new Vector3(0, targetHeight, 0));
		}
	}
}
