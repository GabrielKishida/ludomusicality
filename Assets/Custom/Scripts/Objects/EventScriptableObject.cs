using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Event")]
public class EventScriptableObject : ScriptableObject {
	[SerializeField] private UnityEvent invokedEvent;

	public void OnEnable() {
		if (invokedEvent != null) {
			invokedEvent = new UnityEvent();
		}
	}

	public void AddListener(UnityAction call) {
		invokedEvent.AddListener(call);
	}

	public void Invoke() {
		invokedEvent.Invoke();
	}

	public void OnDestroy() {
		invokedEvent.RemoveAllListeners();
	}
}
