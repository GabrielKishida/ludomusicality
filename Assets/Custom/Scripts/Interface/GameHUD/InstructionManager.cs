using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour {
	[SerializeField] float fadeDuration = 0.5f;
	[Serializable]
	public struct Instruction {
		public AreaDetectorEvent instructionEvent;
		public Image image;
	}

	[SerializeField] public List<Instruction> instructions;

	private bool fadeInOccurring = false;
	private bool fadeOutOccurring = false;

	private IEnumerator FadeOutCoroutine(Instruction instruction) {
		Color originalColor = instruction.image.color;
		fadeOutOccurring = true;
		fadeInOccurring = false;

		for (float t = 0; t < fadeDuration; t += Time.deltaTime) {
			if (fadeInOccurring) {
				break;
			}
			float normalizedTime = t / fadeDuration;
			instruction.image.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, normalizedTime));
			yield return null;
		}
		if (!fadeInOccurring) {
			instruction.image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
		}
		fadeOutOccurring = false;
	}

	private IEnumerator FadeInCoroutine(Instruction instruction) {
		Color originalColor = instruction.image.color;
		fadeInOccurring = true;
		fadeOutOccurring = false;

		for (float t = 0; t < fadeDuration; t += Time.deltaTime) {
			if (fadeOutOccurring) {
				break;
			}
			float normalizedTime = t / fadeDuration;
			instruction.image.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, normalizedTime));
			yield return null;
		}
		if (!fadeOutOccurring) {
			instruction.image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
		}
		fadeInOccurring = false;
	}

	public void Start() {
		foreach (Instruction instruction in instructions) {
			Color uiColor = instruction.image.color;
			uiColor.a = 0f;
			instruction.image.color = uiColor;

			void showInstruction() {
				StartCoroutine(FadeInCoroutine(instruction));
			}
			void hideInstruction() {
				StartCoroutine(FadeOutCoroutine(instruction));
			}
			instruction.instructionEvent.enterAreaEvent.AddListener(showInstruction);
			instruction.instructionEvent.exitAreaEvent.AddListener(hideInstruction);
		}
	}

	public void OnDestroy() {
		foreach (Instruction instruction in instructions) {
			instruction.instructionEvent.enterAreaEvent.RemoveAllListeners();
			instruction.instructionEvent.exitAreaEvent.RemoveAllListeners();
		}
	}
}
