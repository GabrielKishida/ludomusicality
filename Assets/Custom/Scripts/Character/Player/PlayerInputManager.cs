using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour {

	[SerializeField] private Transform playerTransform;

	private PlayerInputActions playerControls;
	[HideInInspector] public InputAction moveInput;
	[HideInInspector] public InputAction rollInput;
	[HideInInspector] public InputAction attackInput;
	[HideInInspector] public InputAction mouseDirectionInput;

	private void Awake() {
		playerControls = new PlayerInputActions();
	}

	private void OnEnable() {
		moveInput = playerControls.Player.Move;
		rollInput = playerControls.Player.Roll;
		attackInput = playerControls.Player.Attack;
		mouseDirectionInput = playerControls.Player.MouseDirection;
		moveInput.Enable();
		rollInput.Enable();
		attackInput.Enable();
		mouseDirectionInput.Enable();

	}

	private void OnDisable() {
		moveInput.Disable();
		rollInput.Disable();
		attackInput.Disable();
		mouseDirectionInput.Disable();
	}

	public Vector2 ReadMovement() {
		return moveInput.ReadValue<Vector2>();
	}

	public Vector2 GetCharacterToMouseDirection() {
		Vector2 mouseCoordinates = mouseDirectionInput.ReadValue<Vector2>();
		Vector2 characterCoordinates = Camera.main.WorldToScreenPoint(playerTransform.position);
		return mouseCoordinates - characterCoordinates;
	}

	public bool IsMovementNull() {
		return ReadMovement() != null;
	}

	public bool IsAttackPressed() {
		return attackInput.WasPressedThisFrame();
	}

	public bool IsRollPressed() {
		return rollInput.WasPressedThisFrame();
	}
}
