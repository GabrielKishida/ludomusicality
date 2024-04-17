using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLightPulse : MonoBehaviour {
	[SerializeField] private float maxIntensity;
	[SerializeField] private Light pulsingLight;
	[SerializeField] private MusicEventScriptableObject playerEvent;

	public void SetLightBright() {
		pulsingLight.intensity = maxIntensity;
	}

	public void Start() {
		playerEvent.musicEvent.AddListener(SetLightBright);
	}
	void Update() {
		pulsingLight.intensity *= 0.95f;
	}
}
