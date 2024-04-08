using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class MusicControlller : MonoBehaviour {
	public AudioClip musicClip;
	int currentStep = 0;

	[SerializeField] private AudioSource musicSource;
	[SerializeField] private float musicTime;
	[SerializeField] private float[] eventTimes;

	public UnityEvent enemyAttack;

	float[] ReadEventTimes() {
		TextAsset textAsset = Resources.Load<TextAsset>("snare_times");
		List<float> floatList = new List<float>();

		if (textAsset != null) {
			string[] lines = textAsset.text.Split('\n');
			foreach (string line in lines) {
				if (float.TryParse(line, out float number)) {
					floatList.Add(number / 1000.0f);
				}
			}
		}
		else {
			Debug.LogError("Text file not found in Resources folder.");
		}
		return floatList.ToArray();
	}

	void Start() {
		musicSource = GetComponent<AudioSource>();
		musicSource.clip = musicClip;
		musicSource.Play();
		eventTimes = ReadEventTimes();
	}

	private void Update() {
		musicTime = (float)musicSource.timeSamples / musicSource.clip.frequency;
		if (musicTime > eventTimes[currentStep]) {
			enemyAttack.Invoke();

			currentStep++;
			if (currentStep >= eventTimes.Length) {
				currentStep = 0;
			}
		}
	}
}
