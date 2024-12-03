using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Custom.Scripts.Objects {
	public class AreaDetector : MonoBehaviour {
		[SerializeField] private bool triggerEnterOnceOnly = false;
		[SerializeField] private List<string> tagsToCheck;
		[SerializeField] private List<EventScriptableObject> enterAreaEvents;
		[SerializeField] private List<EventScriptableObject> exitAreaEvents;

		private bool hasTriggered = false;

		public UnityEvent whenEnterEvent;
		public UnityEvent whenExitEvent;

		private void OnTriggerEnter(Collider collider) {
			if (tagsToCheck.Contains(collider.tag)) {
				if (!(triggerEnterOnceOnly && hasTriggered)) {
					hasTriggered = true;
					whenEnterEvent.Invoke();
					if (enterAreaEvents.Count > 0) {
						foreach (EventScriptableObject enterEvent in enterAreaEvents) {
							enterEvent.Invoke();
						}
					}
				}

			}
		}

		private void OnTriggerExit(Collider collider) {
			if (tagsToCheck.Contains(collider.tag)) {
				whenExitEvent.Invoke();
				if (exitAreaEvents.Count > 0) {
					foreach (EventScriptableObject exitEvent in exitAreaEvents) {
						exitEvent.Invoke();
					}
				}

			}
		}
	}
}
