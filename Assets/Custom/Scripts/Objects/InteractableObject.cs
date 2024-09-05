using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : MonoBehaviour {
	[SerializeField] private EventScriptableObject eventScriptableObject;
	[SerializeField] private bool destroyObjectAfter;
	[SerializeField] private bool interactOnceOnly;

	public void TriggerInteraction() {
		if (eventScriptableObject != null) { eventScriptableObject.Invoke(); }

		if (destroyObjectAfter) { Destroy(gameObject); }

		else if (interactOnceOnly) { Destroy(this); }
	}
}
