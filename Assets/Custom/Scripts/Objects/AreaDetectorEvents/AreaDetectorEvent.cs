using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AreaDetectorEvent", menuName = "ScriptableObjects/AreaDetectorEvent")]
public class AreaDetectorEvent : ScriptableObject {
	[SerializeField] List<string> tagsToCheck;
	[SerializeField] public UnityEvent enterAreaEvent;
	[SerializeField] public UnityEvent exitAreaEvent;

	public bool ContainsTag(string tag) {
		return tagsToCheck.Contains(tag);
	}

	public void TriggerEnterFunction(Collider collider) {
		enterAreaEvent.Invoke();
	}

	public void TriggerExitFunction(Collider collider) {
		exitAreaEvent.Invoke();
	}
}
