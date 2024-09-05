
using UnityEngine;

public class UIFollower : MonoBehaviour {
	[SerializeField] private Transform target;
	[SerializeField] private Vector3 offset;
	[SerializeField] private RectTransform uiElement;
	private Camera mainCamera;


	void Start() {
		mainCamera = Camera.main;
	}

	void Update() {
		if (target == null) return;
		Vector3 screenPosition = mainCamera.WorldToScreenPoint(target.position);
		uiElement.position = screenPosition + offset;
	}
}
