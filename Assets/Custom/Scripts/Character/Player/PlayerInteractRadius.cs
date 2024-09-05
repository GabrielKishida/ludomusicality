
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractRadius : MonoBehaviour {
	[SerializeField] private List<InteractableObject> interactableObjects;

	public void Start() {
		interactableObjects = new List<InteractableObject>();
	}

	public void OnTriggerEnter(Collider other) {
		InteractableObject interactableObject = other.GetComponent<InteractableObject>();
		if (interactableObject != null) {
			interactableObjects.Add(interactableObject);
		}
	}

	public void OnTriggerExit(Collider other) {
		InteractableObject interactableObject = other.GetComponent<InteractableObject>();
		RemoveInteractableFromRadius(interactableObject);
	}

	public InteractableObject GetClosestInteractable() {
		float smallestDistance = 1000000f;
		int selectedIndex = -1;
		for (int i = 0; i < interactableObjects.Count; i++) {
			float distance = Vector3.Distance(interactableObjects[i].transform.position, transform.position);
			if (distance < smallestDistance) {
				selectedIndex = i;
				smallestDistance = distance;
			}
		}
		if (selectedIndex == -1) {
			return null;
		}
		else {
			return interactableObjects[selectedIndex];
		}
	}

	public void RemoveInteractableFromRadius(InteractableObject interactableObject) {
		if (interactableObject != null && interactableObjects.Contains(interactableObject)) {
			interactableObjects.Remove(interactableObject);
		}
	}
}
