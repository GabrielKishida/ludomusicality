using System.Collections.Generic;
using UnityEngine;

public class MultipleEventsTrigger : MonoBehaviour {
	[SerializeField] private List<EventScriptableObject> events = new List<EventScriptableObject>();
	[SerializeField] private List<EventScriptableObject> concludedEvents = new List<EventScriptableObject>();

	[SerializeField] private List<EventScriptableObject> triggerEvents = new List<EventScriptableObject>();

	private void Start() {
		concludedEvents = new List<EventScriptableObject>();
		foreach (EventScriptableObject eventScriptableObject in events) {
			eventScriptableObject.AddListener(() => AddEventToConcluded(eventScriptableObject));
		}
	}

	private void OnDestroy() {
		foreach (EventScriptableObject eventScriptableObject in events) {
			eventScriptableObject.OnDestroy();
		}
	}

	private void AddEventToConcluded(EventScriptableObject eventScriptableObject) {
		if (!concludedEvents.Contains(eventScriptableObject)) {
			concludedEvents.Add(eventScriptableObject);
		}
		Debug.Log(concludedEvents.Count - 1);
		if (triggerEvents[concludedEvents.Count - 1] != null) {
			triggerEvents[concludedEvents.Count - 1].Invoke();
		}
	}
}
