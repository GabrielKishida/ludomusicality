using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : CharacterManager {
	[Header("Event Scriptable Objects")]
	[SerializeField] public PlayerAttackEventScriptableObject attackEvent;
	[SerializeField] private PlayerHealthEventScriptableObject playerHealth;

	public PlayerDashController dashController;

	[SerializeField] private Healthbar healthBar;

	private PlayerInputActions playerControls;
	[HideInInspector] public InputAction moveInput;
	[HideInInspector] public InputAction rollInput;
	[HideInInspector] public InputAction attackInput;
	[HideInInspector] public InputAction mouseDirectionInput;

	public PlayerIdleState idleState;
	public PlayerMoveState moveState;
	public PlayerAttackState attackState;
	public PlayerHurtState hurtState;
	public PlayerDashState dashState;

	[Header("State Machine Variables")]
	[SerializeField] private float hurtStateTimeout = 1.0f;
	[SerializeField] private float dashDuration = 0.1f;

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

	private void SetupStateMachine() {
		idleState = new PlayerIdleState(this);
		moveState = new PlayerMoveState(this);
		attackState = new PlayerAttackState(this);
		hurtState = new PlayerHurtState(this, hurtStateTimeout);
		dashState = new PlayerDashState(this, dashDuration);

		stateMachine = new StateMachine(idleState);
	}

	protected override void Start() {
		base.Start();
		SetupStateMachine();

	}
	public override void OnHurtboxHit(float damage, Vector3 knockback) {
		if (stateMachine.currentState != hurtState) {
			playerHealth.Hurt(damage);
			stateMachine.TransitionTo(hurtState);
			base.OnHurtboxHit(damage, knockback);
		}
	}
}
