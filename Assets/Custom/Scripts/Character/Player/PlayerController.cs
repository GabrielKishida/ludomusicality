using UnityEngine;


public class PlayerController : MonoBehaviour {

	[Header("Event Scriptable Objects")]
	[SerializeField]
	private PlayerHealthEventScriptableObject playerHealth;

	[Header("External Components")]
	[SerializeField] private PlayerMovementController movementController;
	[SerializeField] private PlayerAttackController attackController;
	[SerializeField] private PlayerInputManager inputManager;
	[SerializeField] private PlayerVisualsController visualsController;
	[SerializeField] private PlayerInteractController interactionController;
	[SerializeField] private Hurtbox hurtbox;

	[Header("State Machine")]
	[SerializeField] private StateMachine stateMachine;


	[SerializeField] private PlayerIdleState idleState;
	[SerializeField] private PlayerMoveState moveState;
	[SerializeField] private PlayerHoldAttackState holdAttackState;
	[SerializeField] private PlayerAttackState attackState;
	[SerializeField] private PlayerHurtState hurtState;
	[SerializeField] private PlayerLongHurtState longHurtState;
	[SerializeField] private PlayerDashState dashState;
	[SerializeField] private PlayerInteractState interactState;

	private static PlayerController _instance;

	public static PlayerController Instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<PlayerController>();

				if (_instance == null) {
					GameObject singletonObject = new GameObject();
					_instance = singletonObject.AddComponent<PlayerController>();
					singletonObject.name = typeof(PlayerController).ToString() + " (Singleton)";
					DontDestroyOnLoad(singletonObject);
				}
			}
			return _instance;
		}
	}

	private void Awake() {
		if (_instance == null) {
			_instance = this;
		}
		else if (_instance != this) {
			Destroy(gameObject);
		}
	}

	private PlayerStateBase GetNextState(int nextStateNum) {
		switch ((PlayerState)nextStateNum) {
			case PlayerState.PlayerIdleState: return idleState;
			case PlayerState.PlayerMoveState: return moveState;
			case PlayerState.PlayerDashState: return dashState;
			case PlayerState.PlayerHoldAttackState: return holdAttackState;
			case PlayerState.PlayerAttackState: return attackState;
			case PlayerState.PlayerHurtState: return hurtState;
			case PlayerState.PlayerInteractState: return interactState;
			default: return idleState;
		}
	}

	private void Start() {
		idleState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		moveState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		holdAttackState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		attackState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		hurtState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		longHurtState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		dashState.Setup(movementController, attackController, inputManager, visualsController, interactionController);
		interactState.Setup(movementController, attackController, inputManager, visualsController, interactionController);

		stateMachine.Setup(idleState);

		playerHealth.ResetHealth();
		hurtbox.hurtboxHitEvent.AddListener(HurtPlayer);
	}

	private void Update() {
		stateMachine.Do();
		if (stateMachine.currentState.isComplete) {
			stateMachine.TransitionTo(GetNextState(stateMachine.currentState.nextStateNum));
		}
	}

	public void HurtPlayer(float damage, Vector3 knockback) {
		if (!hurtState.IsInvulnerable() && !longHurtState.IsInvulnerable()) {
			if (stateMachine.currentState != hurtState && stateMachine.currentState != longHurtState) {
				movementController.ReceiveKnockback(knockback);
				playerHealth.Hurt(damage);
				CameraShake.Instance.ApplyCameraShake(damage);
				stateMachine.TransitionTo(hurtState);
			}
		}
	}

	public void LongHurtPlayer(float damage, Vector3 knockback) {
		if (!hurtState.IsInvulnerable() && !longHurtState.IsInvulnerable()) {
			movementController.ReceiveKnockback(knockback);
			if (stateMachine.currentState != hurtState && stateMachine.currentState != longHurtState) {
				playerHealth.Hurt(damage);
				CameraShake.Instance.ApplyCameraShake(damage);
				stateMachine.TransitionTo(longHurtState);
			}
		}
	}

	public void DisablePlayer() {
		inputManager.DisablePlayer();
		movementController.shouldCaptureSafeSpot = false;
		movementController.SetHorizontalSpeed(Vector2.zero);
	}

	public void EnablePlayer() {
		inputManager.EnablePlayer();
		movementController.shouldCaptureSafeSpot = true;
	}

	public void TeleportPlayer(Vector3 newPosition) {
		movementController.SetPositionAs(newPosition);
	}

	public void TeleportPlayerToSafeSpot() {
		movementController.TeleportToSafeSpot();
	}

}
