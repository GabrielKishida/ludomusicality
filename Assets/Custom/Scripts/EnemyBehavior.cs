using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MovementController {

    [SerializeField] private Transform target;  // The target GameObject to follow
    [SerializeField] private float aimSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileCooldown;
    private bool isOnCooldown = false;

    private void RotateTowardsTarget() {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, aimSpeed * Time.deltaTime);
    }

    private IEnumerator ShootCoroutine() {
        isOnCooldown = true;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.velocity = transform.forward * projectileSpeed;
        }
        yield return new WaitForSeconds(projectileCooldown);
        isOnCooldown = false;

    }

    protected override void Update() {
        if (!isOnCooldown) {
            StartCoroutine(ShootCoroutine());
        }
        RotateTowardsTarget();
        base.Update();
    }
}
