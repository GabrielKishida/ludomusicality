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

	[SerializeField] public UnityEvent musicEvent;
	[SerializeField] public bool reachedEnd;

	public void OnEnable() {
		this.currentStep = 0;
		reachedEnd = false;
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

		if (time < 1.0 && reachedEnd) {
			reachedEnd = false;
		}

		if (time > eventTimes[currentStep] && !reachedEnd) {
			musicEvent.Invoke();
			currentStep++;
			if (currentStep >= eventTimes.Length) {
				currentStep = 0;
				reachedEnd = true;
			}
		}
	}

	public bool CheckEventNearTriggerTime(float timeThreshold) {
		bool nearCurrentStep = Math.Abs(musicTime - eventTimes[currentStep]) < timeThreshold;
		bool nearPreviousStep = false;
		bool nearNextStep = false;
		if (currentStep > 0) {
			nearPreviousStep = Math.Abs(musicTime - eventTimes[currentStep - 1]) < timeThreshold;
		}
		else if (currentStep <= eventTimes.Length) {
			nearNextStep = Math.Abs(musicTime - eventTimes[currentStep + 1]) < timeThreshold;
		}
		return nearCurrentStep || nearPreviousStep || nearNextStep;
	}
}
