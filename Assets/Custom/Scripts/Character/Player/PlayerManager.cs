using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : CharacterManager {
	public PlayerAttackController attackController;
	public PlayerDashController dashController;

	[SerializeField] private Healthbar healthBar;

	private PlayerInputActions playerControls;
	[HideInInspector] public InputAction move;
	[HideInInspector] public InputAction roll;
	[HideInInspector] public InputAction attack;
	[HideInInspector] public InputAction mouseDirection;

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
		move = playerControls.Player.Move;
		roll = playerControls.Player.Roll;
		attack = playerControls.Player.Attack;
		mouseDirection = playerControls.Player.MouseDirection;
		move.Enable();
		roll.Enable();
		attack.Enable();
		mouseDirection.Enable();

	}

	private void OnDisable() {
		move.Disable();
		roll.Disable();
		attack.Disable();
		mouseDirection.Disable();
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
		attackController = GetComponent<PlayerAttackController>();
		SetupStateMachine();

	}
	public override void OnHurtboxHit(float damage, Vector3 knockback) {
		if (stateMachine.currentState != hurtState) {
			healthBar.TakeDamage(damage);
			stateMachine.TransitionTo(hurtState);
			base.OnHurtboxHit(damage, knockback);
		}
	}
}
