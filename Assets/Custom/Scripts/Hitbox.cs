using UnityEngine;
using UnityEngine.TextCore.Text;

public class Hitbox : MonoBehaviour {
    public float knockbackForce = 5.0f;
    private void OnTriggerEnter(Collider hurtboxCollider) {
        Hurtbox hurtbox = hurtboxCollider.GetComponent<Hurtbox>();
        if (hurtbox != null) {
            Rigidbody rb = hurtboxCollider.GetComponent<Rigidbody>();
            if (rb != null) {
                Vector3 knockbackDirection = (hurtbox.transform.position - transform.parent.position).normalized;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            }
            CharacterController charController = hurtboxCollider.GetComponent<CharacterController>();
            if (charController != null) {

            }
        }

    }
}
