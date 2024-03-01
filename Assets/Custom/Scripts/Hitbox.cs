using UnityEngine;

public class Hitbox : MonoBehaviour {
    public float knockbackForce = 5.0f;
    private void OnTriggerEnter(Collider hurtboxCollider) {
        Hurtbox hurtbox = hurtboxCollider.GetComponent<Hurtbox>();
        if (hurtbox != null) {
            Rigidbody rb = hurtboxCollider.GetComponent<Rigidbody>();
            Vector3 knockbackDirection = (hurtbox.transform.position - transform.parent.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }

    }
}
