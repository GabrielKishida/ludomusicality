using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : MonoBehaviour {

	private float initialHeight;

	private void Start() {
		initialHeight = gameObject.transform.position.y;
	}

	void FixedUpdate() {
		Quaternion currentRotation = gameObject.transform.rotation;
		gameObject.transform.rotation = Quaternion.Euler(0, currentRotation.eulerAngles.y, 0);
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, initialHeight, gameObject.transform.position.z);
	}
}
