
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractController : MonoBehaviour {
	[SerializeField] private PlayerInteractRadius interactRadius;
	[SerializeField] private float interactDuration;
	[SerializeField] private float interactTime;
	[SerializeField] private float interactCompletion;

	[SerializeField] private bool isInteracting;
	[SerializeField] private Slider slider;

	[SerializeField] private bool recentlyFinished = false;

	[SerializeField] private InteractableObject lastInteractedObject;


	void Start() {
		ResetInteraction();
	}

	void Update() {
		if (isInteracting) {
			if (recentlyFinished) {
				return;
			}
			InteractableObject interactedObject = interactRadius.GetClosestInteractable();
			if (interactedObject == null) {
				SetIsInteracting(false);
				return;
			}
			else {
				if (lastInteractedObject == null) {
					lastInteractedObject = interactedObject;
				}
				else if (lastInteractedObject != interactedObject) {
					lastInteractedObject = interactedObject;
					ResetInteraction();
				}
			}
			interactCompletion = interactTime / interactDuration;
			if (interactTime >= interactDuration) {
				slider.value = 1;
				FinishInteraction();
			}
			else {
				interactTime += Time.deltaTime;
			}
			slider.value = interactCompletion;
		}
		else if (interactCompletion > 0) {
			interactCompletion = interactTime / interactDuration;
			interactTime -= Time.deltaTime;
			if (interactTime <= 0) {
				interactTime = 0;
				interactCompletion = 0;
				// init fadeout routine
			}
			slider.value = interactCompletion;
		}
	}

	public void ResetInteraction() {
		isInteracting = false;
		interactCompletion = 0f;
		interactTime = 0f;
		slider.value = interactCompletion;
	}

	public bool CanInteract() {
		return interactRadius.GetClosestInteractable() != null;
	}

	public bool RecentlyFinishedInteraction() {
		return recentlyFinished;
	}

	public void FinishInteraction() {
		InteractableObject interactableObject = interactRadius.GetClosestInteractable();
		interactRadius.RemoveInteractableFromRadius(interactableObject);
		interactableObject.TriggerInteraction();
		recentlyFinished = true;
		ResetInteraction();
	}

	public void SetIsInteracting(bool isInteracting) {
		if (isInteracting) {
			recentlyFinished = false;
		}
		this.isInteracting = isInteracting;
	}

}
