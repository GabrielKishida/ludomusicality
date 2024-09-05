using System.Collections.Generic;
using UnityEngine;

namespace Assets.Custom.Scripts.Objects {
	public class AreaDetector : MonoBehaviour {
		[SerializeField] private List<string> tagsToCheck;
		[SerializeField] private List<EventScriptableObject> enterAreaEvents;
		[SerializeField] private List<EventScriptableObject> exitAreaEvents;

		private void OnTriggerEnter(Collider collider) {
			if (tagsToCheck.Contains(collider.tag)) {
				foreach (EventScriptableObject enterEvent in enterAreaEvents) {
					enterEvent.Invoke();
				}
			}
		}

		private void OnTriggerExit(Collider collider) {
			if (tagsToCheck.Contains(collider.tag)) {
				foreach (EventScriptableObject exitEvent in exitAreaEvents) {
					exitEvent.Invoke();
				}
			}
		}
	}
}
