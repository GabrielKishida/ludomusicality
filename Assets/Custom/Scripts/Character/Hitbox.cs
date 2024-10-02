using UnityEngine;
using UnityEngine.TextCore.Text;

public class Hitbox : MonoBehaviour {
	[SerializeField] protected float knockbackForce = 5.0f;
	[SerializeField] protected float damage = 1.0f;
	[SerializeField] protected string immuneTag;
	[SerializeField] protected bool isFromPlayer = false;
	public Vector3 GetKnockbackByPosition(Collider collider) {
		Vector3 knockback;
		if (transform.parent != null) {
			knockback = (collider.transform.position - transform.parent.position).normalized * knockbackForce;
		}
		else {
			knockback = (collider.transform.position - transform.position).normalized * knockbackForce;
		}
		return knockback;
	}

	public void ApplyKnockback(Collider collider, Vector3 knockback) {
		Rigidbody rb = collider.GetComponent<Rigidbody>();
		Hurtbox hurtbox = collider.GetComponent<Hurtbox>();

		if (rb != null) {
			rb.AddForce(knockback, ForceMode.Impulse);
		}
		if (hurtbox != null) {
			hurtbox.OnTakeDamage(damage, knockback);
		}
		if ((hurtbox != null || rb != null) && isFromPlayer) {
			CameraShake.Instance.ApplyCameraShake(damage * 0.5f);
		}
	}

	private void OnTriggerEnter(Collider collider) {
		if (immuneTag != null) {
			if (collider.CompareTag(immuneTag)) {
				return;
			}
		}
		Vector3 knockback = GetKnockbackByPosition(collider);
		ApplyKnockback(collider, knockback);
	}
}
