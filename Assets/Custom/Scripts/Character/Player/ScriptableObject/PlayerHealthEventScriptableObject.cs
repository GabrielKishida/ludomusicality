using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "PlayerHealthEventScriptableObject", menuName = "ScriptableObjects/PlayerHealthEvent")]
public class PlayerHealthEventScriptableObject : ScriptableObject {
	[SerializeField] public float currentHp;
	[SerializeField] public float maxHp;

	[SerializeField] public UnityEvent<float> healthEvent;
	[SerializeField] public UnityEvent deathEvent;

	public void SetHealthTo(float health) {
		currentHp = health;
		healthEvent.Invoke(health);
	}

	public void ResetHealth() {
		currentHp = maxHp;
	}

	public void Heal(float healValue) {
		currentHp = Math.Min(currentHp + healValue, maxHp);
		healthEvent.Invoke(Math.Min(currentHp, maxHp));
	}

	public void Hurt(float damage) {
		currentHp = Math.Max(currentHp - damage, 0);
		healthEvent.Invoke(currentHp);
		if (currentHp == 0) {
			deathEvent.Invoke();
		}
	}
}
