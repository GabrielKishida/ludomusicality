using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Instruction {
	public EventScriptableObject showInstructionEvent;
	public EventScriptableObject hideInstructionEvent;
	public Image image;
	public bool visible = false;

	public void SetVisibility(bool visible) {
		this.visible = visible;
	}

}


public class InstructionManager : MonoBehaviour {
	[SerializeField] float fadeDuration = 0.5f;

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
			instruction.SetVisibility(false);

			void showInstruction() {
				if (!instruction.visible) { StartCoroutine(FadeInCoroutine(instruction)); }
				instruction.SetVisibility(true);
			}

			void hideInstruction() {
				if (instruction.visible) {
					StartCoroutine(FadeOutCoroutine(instruction));
					instruction.SetVisibility(false);
				}
			}
			instruction.showInstructionEvent.AddListener(showInstruction);
			instruction.hideInstructionEvent.AddListener(hideInstruction);
		}
	}

	public void OnDestroy() {
		foreach (Instruction instruction in instructions) {
			instruction.showInstructionEvent.OnDestroy();
			instruction.hideInstructionEvent.OnDestroy();
		}
	}
}
