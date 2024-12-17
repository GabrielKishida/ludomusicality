using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using System.Globalization;

[CreateAssetMenu(fileName = "MusicEventScriptableObject", menuName = "ScriptableObjects/MusicEvent")]
public class MusicEventScriptableObject : ScriptableObject {
	[SerializeField] private int currentStep = 0;
	[SerializeField] private float[] eventTimes;
	[SerializeField] private float musicTime;
	[SerializeField] private TextAsset timeFile;

	[SerializeField] public UnityEvent musicEvent;
	[SerializeField] public bool reachedEnd;

	public void OnEnable() {
		Reset();
		eventTimes = ReadEventTimes();
	}

	public void Reset() {
		this.currentStep = 0;
		reachedEnd = false;
	}

	float[] ReadEventTimes() {
		List<float> floatList = new List<float>();

		if (timeFile != null) {
			string[] lines = timeFile.text.Split('\n');
			foreach (string line in lines) {
				if (float.TryParse(line, NumberStyles.Float, CultureInfo.InvariantCulture, out float number)) {
					floatList.Add(number);
				}
			}
		}
		return floatList.ToArray();
	}

	public void UpdateMusicTime(float time) {
		musicTime = time;

		if (time < 1.0 && reachedEnd) {
			reachedEnd = false;
		}

		if (time > eventTimes[currentStep] - 0.1f && !reachedEnd) {
			musicEvent.Invoke();
			currentStep++;

			if (currentStep >= eventTimes.Length) {
				currentStep = 0;
				reachedEnd = true;
			}
		}
	}

	public bool CheckEventNearTriggerTime(float timeThreshold) {
		bool nearCurrentStep = Math.Abs(musicTime - eventTimes[currentStep] + 0.1f) < timeThreshold;
		bool nearPreviousStep = false;
		bool nearNextStep = false;
		if (currentStep > 0) {
			nearPreviousStep = Math.Abs(musicTime - eventTimes[currentStep - 1] + 0.1f) < timeThreshold;
		}
		else if (currentStep <= eventTimes.Length) {
			nearNextStep = Math.Abs(musicTime - eventTimes[currentStep + 1] + 0.1f) < timeThreshold;
		}
		return nearCurrentStep || nearPreviousStep || nearNextStep;
	}
}
