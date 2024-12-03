
using Cinemachine;
using UnityEngine;

public class VirtualCameraArea : MonoBehaviour {
	[SerializeField] private CinemachineVirtualCamera virtualCamera;
	[SerializeField] private bool areaEnabled = true;
	[SerializeField] private EventScriptableObject disableAreaEventToListen;
	[SerializeField] private EventScriptableObject enterAreaEventToInvoke;
	[SerializeField] private bool disableCameraWhenLeft = true;

	[SerializeField] private bool hasEnterTriggered = false;

	private void DisableArea() {
		areaEnabled = false;
		virtualCamera.enabled = false;
	}

	private void Start() {
		hasEnterTriggered = false;
		if (disableAreaEventToListen != null) { disableAreaEventToListen.AddListener(DisableArea); }
	}

	private void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("Player") && areaEnabled) {
			if (!hasEnterTriggered) {
				if (enterAreaEventToInvoke != null) { enterAreaEventToInvoke.Invoke(); }
				hasEnterTriggered = true;
			}
			virtualCamera.enabled = true;
		};
	}

	private void OnTriggerExit(Collider collider) {
		if (collider.CompareTag("Player") && areaEnabled && disableCameraWhenLeft) virtualCamera.enabled = false;
	}
}
