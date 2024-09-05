
using Cinemachine;
using UnityEngine;

public class VirtualCameraArea : MonoBehaviour {
	[SerializeField] CinemachineVirtualCamera virtualCamera;


	private void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("Player")) virtualCamera.enabled = true;

	}

	private void OnTriggerExit(Collider collider) {
		if (collider.CompareTag("Player")) virtualCamera.enabled = false;
	}
}
