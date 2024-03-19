using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDeathController : MonoBehaviour {

	[SerializeField] private float timeToDespawn = 1.0f;
	public void Die() {
		Destroy(gameObject, timeToDespawn);
	}
}
