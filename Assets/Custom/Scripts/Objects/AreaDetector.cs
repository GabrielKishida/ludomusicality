using System.Collections.Generic;
using UnityEngine;

namespace Assets.Custom.Scripts.Objects {
	public class AreaDetector : MonoBehaviour {
		[SerializeField] private List<AreaDetectorEvent> areaEvents;

		private void OnTriggerEnter(Collider collider) {
			foreach (AreaDetectorEvent areaEvent in areaEvents) {
				if (areaEvent.ContainsTag(collider.tag)) {
					areaEvent.TriggerEnterFunction(collider);
				}
			}
		}

		private void OnTriggerExit(Collider collider) {
			foreach (AreaDetectorEvent areaEvent in areaEvents) {
				if (areaEvent.ContainsTag(collider.tag)) {
					areaEvent.TriggerExitFunction(collider);
				}
			}
		}
	}
}
