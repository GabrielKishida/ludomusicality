using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using System.Collections;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Event")]
public class EventScriptableObject : ScriptableObject {
	[SerializeField] protected UnityEvent invokedEvent;
	[SerializeField] protected float timeToTrigger = 0;
	[SerializeField] protected bool debugWhenCalled = false;

	virtual public void OnEnable() {
		if (invokedEvent == null) {
			invokedEvent = new UnityEvent();
		}
	}

	public void AddListener(UnityAction call) {
		if (invokedEvent == null) {
			invokedEvent = new UnityEvent();
		}
		invokedEvent.AddListener(call);
	}
	public virtual void Invoke() {
		CoroutineHelper.Instance.RunCoroutine(InvokeCoroutine());
	}

	private IEnumerator InvokeCoroutine() {
		if (debugWhenCalled) {
			Debug.Log(this.name + " was called.");
		}
		if (timeToTrigger > 0) {
			yield return new WaitForSeconds(timeToTrigger);
		}
		invokedEvent.Invoke();
	}

	public void OnDestroy() {
		invokedEvent.RemoveAllListeners();
	}
}
