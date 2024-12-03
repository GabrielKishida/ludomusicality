using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

[CreateAssetMenu(fileName = "MultipleSimultaneousEvents", menuName = "ScriptableObjects/MultipleSimultaneousEvents")]
public class MultipleSimultaneousEvents : EventScriptableObject {
	[SerializeField] private List<EventScriptableObject> events;

	public override async void Invoke() {
		CoroutineHelper.Instance.RunCoroutine(InvokeCoroutine());
	}

	private IEnumerator InvokeCoroutine() {
		if (debugWhenCalled) {
			Debug.Log(this.name + " was called.");
		}
		if (timeToTrigger > 0) {
			yield return new WaitForSeconds(timeToTrigger);
		}
		foreach (EventScriptableObject e in events) {
			e.Invoke();
		}
	}
}
