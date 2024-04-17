using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "PlayerHealthEventScriptableObject", menuName = "ScriptableObjects/PlayerHealthEvent")]
public class PlayerHealthEventScriptableObject : ScriptableObject {
	[SerializeField] public float currentHp;
	[SerializeField] public float maxHp;

	[SerializeField] public UnityEvent<float> healthEvent;

	public void SetHealthTo(float health) {
		healthEvent.Invoke(health);
	}

	public void Heal(float healValue) {
		healthEvent.Invoke(Math.Min(currentHp + healValue, maxHp));
	}

	public void Hurt(float damage) {
		healthEvent.Invoke(Math.Max(currentHp - damage, 0));
	}
}
