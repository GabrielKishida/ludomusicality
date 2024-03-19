using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : CharacterManager {
	public PlayerAttackController attackController;
	[SerializeField] private Healthbar health;

	private PlayerInputActions playerControls;
	public InputAction move;
	public InputAction roll;
	public InputAction attack;
	public InputAction mouseDirection;

	private IState idleState;
	private IState moveState;
	private IState attackState;
	private IState hurtState;

	public override void OnHurtboxHit(float damage, Vector3 knockback) {
		health.TakeDamage(damage);
		base.OnHurtboxHit(damage, knockback);
	}

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

	protected override void Start() {
		base.Start();
		attackController = GetComponent<PlayerAttackController>();

		idleState = new PlayerIdleState(this);
		moveState = new PlayerMoveState(this);
		attackState = new PlayerAttackState(this);
		hurtState = new PlayerHurtState(this);
		stateMachine = new StateMachine(idleState);
	}

	private bool ShouldAttack() {
		bool canAttack = !attackController.isAttacking && !attackController.isOnCooldown;
		return canAttack && attack.ReadValue<float>() == 1; ;
	}

	private bool ShouldMove() {
		return !attackController.isAttacking && move.ReadValue<Vector2>() != Vector2.zero;
	}

	private bool ShouldIdle() {
		return !attackController.isAttacking && move.ReadValue<Vector2>() == Vector2.zero;
	}

	private bool ShouldEndAttack() {
		return !attackController.isAttacking;
	}

	protected override void Update() {
		if (stateMachine.currentState == idleState) {
			if (ShouldAttack()) { stateMachine.TransitionTo(attackState); }
			else if (ShouldMove()) { stateMachine.TransitionTo(moveState); }
		}
		else if (stateMachine.currentState == moveState) {
			if (ShouldAttack()) { stateMachine.TransitionTo(attackState); }
			if (ShouldIdle()) { stateMachine.TransitionTo(idleState); }
		}
		else if (stateMachine.currentState == attackState) {
			if (ShouldEndAttack()) { stateMachine.TransitionTo(idleState); }
		}
		else if (stateMachine.currentState == hurtState) {

		}
		base.Update();
	}
}
