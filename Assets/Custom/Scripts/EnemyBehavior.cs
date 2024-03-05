using System.Collections;
using UnityEngine;

public class EnemyBehaviour : BaseCharacter {

    public Transform target;  // The target GameObject to follow
    public float aimSpeed;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed;

    public float projectileCooldown;
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