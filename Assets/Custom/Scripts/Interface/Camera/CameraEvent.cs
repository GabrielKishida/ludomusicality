
using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraEvent : MonoBehaviour {
	[System.Serializable]
	public struct CameraFocus {
		public CinemachineVirtualCamera virtualCamera;
		public float enabledDuration;
	}
	[SerializeField] EventScriptableObject eventScriptableObject;
	public CameraFocus[] cameraFocuses;
	private void Start() {
		if (eventScriptableObject != null) { eventScriptableObject.AddListener(TriggerCameraFocus); }
	}

	IEnumerator FocusOnVirtualCameraRoutine(CameraFocus[] cameraFocuses) {
		foreach (CameraFocus cameraFocus in cameraFocuses) {
			cameraFocus.virtualCamera.enabled = true;
			yield return new WaitForSeconds(cameraFocus.enabledDuration);
			cameraFocus.virtualCamera.enabled = false;
		}
	}

	public void TriggerCameraFocus() {
		StartCoroutine(FocusOnVirtualCameraRoutine(cameraFocuses));
	}

}
