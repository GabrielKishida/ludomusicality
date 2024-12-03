using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour {

	[SerializeField] private Transform playerTransform;

	private PlayerInputActions playerControls;
	[HideInInspector] private InputAction moveInput;
	[HideInInspector] private InputAction rollInput;
	[HideInInspector] private InputAction attackInput;
	[HideInInspector] private InputAction mouseDirectionInput;
	[HideInInspector] private InputAction interactInput;

	[SerializeField] private bool playerEnabled = true;

	private void Awake() {
		playerControls = new PlayerInputActions();
	}

	private void OnEnable() {
		moveInput = playerControls.Player.Move;
		rollInput = playerControls.Player.Roll;
		attackInput = playerControls.Player.Attack;
		mouseDirectionInput = playerControls.Player.MouseDirection;
		interactInput = playerControls.Player.Interact;
		moveInput.Enable();
		rollInput.Enable();
		attackInput.Enable();
		mouseDirectionInput.Enable();
		interactInput.Enable();

	}

	private void OnDisable() {
		moveInput.Disable();
		rollInput.Disable();
		attackInput.Disable();
		mouseDirectionInput.Disable();
		interactInput.Disable();
	}
	public Vector2 ReadMovement() {
		return playerEnabled ? moveInput.ReadValue<Vector2>() : Vector2.zero;
	}

	public Vector2 GetCharacterToMouseDirection() {
		Vector2 mouseCoordinates = mouseDirectionInput.ReadValue<Vector2>();
		Vector2 characterCoordinates = Camera.main.WorldToScreenPoint(playerTransform.position);
		return mouseCoordinates - characterCoordinates;
	}

	public bool IsMovementZero() {
		return ReadMovement() != Vector2.zero;
	}

	public bool IsAttackPressed() {
		return playerEnabled ? attackInput.IsPressed() : false;
	}

	public bool IsAttackReleased() {
		return playerEnabled ? attackInput.WasReleasedThisFrame() : false;
	}

	public bool WasRollPressed() {
		return playerEnabled ? rollInput.WasPressedThisFrame() : false;
	}

	public bool WasInteractPressed() {
		return playerEnabled ? interactInput.WasPressedThisFrame() : false;
	}

	public bool IsInteractPressed() {
		return playerEnabled ? interactInput.IsPressed() : false;
	}

	public void EnablePlayer() {
		playerEnabled = true;
	}

	public void DisablePlayer() {
		playerEnabled = false;
	}

}
