using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "PlayerAttackEventScriptableObject", menuName = "ScriptableObjects/PlayerAttackEvent")]
public class PlayerAttackEventScriptableObject : ScriptableObject {
	[SerializeField] public bool isOccurring;
	[SerializeField] public bool isOnCooldown;
	[SerializeField] public Vector2 lastDirection;

	[SerializeField] public UnityEvent<Vector2> attackEvent;

	public bool CanAttack() {
		return !isOccurring && !isOnCooldown;
	}

	public void Invoke(Vector2 direction) {
		if (CanAttack()) {
			attackEvent.Invoke(direction);
			lastDirection = direction;
		}
	}
}
