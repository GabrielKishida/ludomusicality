using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum InstructionBehavior {
	ShouldDisableWhenLeft,
	ShouldHideWhenLeft,
	ShouldKeepShowingWhenLeft,
}


namespace Assets.Custom.Scripts.Objects {
	public class InstructionArea : MonoBehaviour {
		[SerializeField] private EventScriptableObject enableInstructionAreaEvent;
		[SerializeField] private EventScriptableObject enableInstructionEvent;
		[SerializeField] private EventScriptableObject hideInstructionEvent;
		[SerializeField] private EventScriptableObject disableInstructionEvent;
		[SerializeField] private InstructionBehavior instructionBehavior = InstructionBehavior.ShouldDisableWhenLeft;
		[SerializeField] private bool disabled = false;

		public void Start() {
			if (disableInstructionEvent != null) { disableInstructionEvent.AddListener(DisableInstruction); }
			if (enableInstructionAreaEvent != null) {
				this.disabled = true;
				enableInstructionAreaEvent.AddListener(EnableInstructionArea);
			}
		}

		private void HideInstruction() {
			if (hideInstructionEvent != null) { hideInstructionEvent.Invoke(); }
			else { Debug.Log("This instruction " + this.name + " has no Hide Event"); }
		}
		private void EnableInstructionArea() {
			disabled = false;
		}

		private void DisableInstruction() {
			HideInstruction();
			disabled = true;
		}

		private void OnTriggerEnter(Collider collider) {
			if (collider.CompareTag("Player") && !disabled) {
				enableInstructionEvent.Invoke();
			}
		}

		private void OnTriggerExit(Collider collider) {
			if (collider.CompareTag("Player") && !disabled) {
				switch (instructionBehavior) {
					case InstructionBehavior.ShouldDisableWhenLeft:
						DisableInstruction();
						break;
					case InstructionBehavior.ShouldHideWhenLeft:
						HideInstruction();
						break;
					case InstructionBehavior.ShouldKeepShowingWhenLeft:
						break;
				}
			}
		}
	}
}
