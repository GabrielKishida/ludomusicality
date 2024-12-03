using UnityEngine;
using UnityEngine.Events;

public class Hurtbox : MonoBehaviour {
	public UnityEvent<float, Vector3> hurtboxHitEvent;

	public void OnTakeDamage(float damage, Vector3 knockback) {
		hurtboxHitEvent.Invoke(damage, knockback);
	}
}
