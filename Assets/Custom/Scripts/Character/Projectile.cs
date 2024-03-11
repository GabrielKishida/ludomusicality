using UnityEngine;

public class Projectile : Hitbox {

	Rigidbody rb;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	private Vector3 GetKnockbackBySpeed() {
		return rb.velocity.normalized * knockbackForce;
	}

	void OnTriggerEnter(Collider collider) {
		Vector3 knockback = GetKnockbackBySpeed();
		ApplyKnockback(collider, knockback);
		Destroy(gameObject, 0.0f);
	}
}
