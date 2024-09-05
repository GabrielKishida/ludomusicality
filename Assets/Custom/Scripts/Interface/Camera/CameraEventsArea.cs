
using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraEventsArea : MonoBehaviour {
	[System.Serializable]
	public struct CameraEvent {
		public CinemachineVirtualCamera virtualCamera;
		public float enabledDuration;
	}

	public CameraEvent[] cameraEvents;

	IEnumerator FocusOnVirtualCameraRoutine(CameraEvent[] cameraEvents) {
		foreach (CameraEvent cameraEvent in cameraEvents) {
			cameraEvent.virtualCamera.enabled = true;
			yield return new WaitForSeconds(cameraEvent.enabledDuration);
			cameraEvent.virtualCamera.enabled = false;
		}
	}

	public void OnCameraEventsTrigger() {
		StartCoroutine(FocusOnVirtualCameraRoutine(cameraEvents));
	}

	private void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("Player")) OnCameraEventsTrigger();

	}

}
