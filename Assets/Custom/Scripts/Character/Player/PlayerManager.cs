using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour {
	[SerializeField] private StateMachine playerStateMachine;

	public MovementController moveController;
	public PlayerAttackController attackController;

	private PlayerInputActions playerControls;
	public InputAction move;
	public InputAction roll;
	public InputAction attack;
	public InputAction mouseDirection;

	private IState idleState;
	private IState moveState;
	private IState attackState;
	private IState hurtState;

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

	protected void Start() {
		moveController = GetComponent<MovementController>();
		attackController = GetComponent<PlayerAttackController>();

		idleState = new PlayerIdleState(this);
		moveState = new PlayerMoveState(this);
		attackState = new PlayerAttackState(this);
		hurtState = new PlayerHurtState(this);
		playerStateMachine = new StateMachine(idleState);
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

	protected void Update() {
		if (playerStateMachine.currentState == idleState) {
			if (ShouldAttack()) { playerStateMachine.TransitionTo(attackState); }
			else if (ShouldMove()) { playerStateMachine.TransitionTo(moveState); }
		}
		else if (playerStateMachine.currentState == moveState) {
			if (ShouldAttack()) { playerStateMachine.TransitionTo(attackState); }
			if (ShouldIdle()) { playerStateMachine.TransitionTo(idleState); }
		}
		else if (playerStateMachine.currentState == attackState) {
			if (ShouldEndAttack()) { playerStateMachine.TransitionTo(idleState); }
		}
		else if (playerStateMachine.currentState == hurtState) {

		}
		playerStateMachine.Update();
	}
}
