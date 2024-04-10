using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "MusicEventScriptableObject", menuName = "ScriptableObjects/MusicEvent")]
public class MusicEventScriptableObject : ScriptableObject {
	[SerializeField] private string timeFileName;
	[SerializeField] private int currentStep = 0;
	[SerializeField] private float[] eventTimes;
	[SerializeField] private float musicTime;
	[SerializeField] private float timeThreshold;

	[SerializeField] public UnityEvent musicEvent;

	public void OnEnable() {
		this.currentStep = 0;
		eventTimes = ReadEventTimes(timeFileName);
	}

	float[] ReadEventTimes(string timeFileName) {
		TextAsset textAsset = Resources.Load<TextAsset>(timeFileName);
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
			Debug.LogError("Text file " + timeFileName + " not found in Resources folder.");
		}
		return floatList.ToArray();
	}

	public void UpdateMusicTime(float time) {
		musicTime = time;
		if (time > eventTimes[currentStep]) {
			musicEvent.Invoke();

			currentStep++;
			if (currentStep >= eventTimes.Length) {
				currentStep = 0;
			}
		}
	}

	public bool CheckEventTriggerNearTime() {
		if (Math.Abs(musicTime - eventTimes[currentStep]) < timeThreshold) { return true; }
		else if (Math.Abs(musicTime - eventTimes[currentStep - 1]) < timeThreshold) { return true; }
		return false;
	}
}
