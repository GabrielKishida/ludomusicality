using UnityEngine;

public class Hurtbox : MonoBehaviour {
    Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider col) {
        if (col.CompareTag("PlayerAttack")) {
            Hitbox hitbox = col.gameObject.GetComponent<Hitbox>();
            Vector3 knockbackDirection = (transform.position - col.transform.parent.position).normalized;
            rb.AddForce(knockbackDirection * hitbox.knockbackForce, ForceMode.Impulse);
        }
    }
}
