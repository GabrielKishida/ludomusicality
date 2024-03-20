using UnityEngine;
using UnityEngine.TextCore.Text;

public class Hurtbox : MonoBehaviour {
	[SerializeField] private CharacterManager characterManager;

	public void OnTakeDamage(float damage, Vector3 knockback) {
		characterManager.OnHurtboxHit(damage, knockback);
	}

	private void Start() {
		characterManager = GetComponent<CharacterManager>();
	}
}
