using UnityEngine;

public class Projectile : MonoBehaviour {
    public int damage;
    private void OnTriggerEnter(Collider collider) {
        Hurtbox hurtbox = collider.GetComponent<Hurtbox>();
        if (hurtbox != null) {

            Destroy(gameObject, 0.0f);
        }


    }
}
