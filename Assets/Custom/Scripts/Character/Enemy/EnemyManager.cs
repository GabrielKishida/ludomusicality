using System.Collections;
using UnityEngine;

public class EnemyManager : CharacterManager {
	public EnemyAttackController attackController;
	public EnemyDeathController deathController;

	private IState lookState;
	private IState shootState;
	private IState deathState;

	[Header("Health")]
	[SerializeField] private float maxHealth;
	[SerializeField] private float currentHealth;

	public override void OnHurtboxHit(float damage, Vector3 knockback) {
		currentHealth -= damage;
		base.OnHurtboxHit(damage, knockback);
	}


	protected override void Start() {
		base.Start();
		attackController = GetComponent<EnemyAttackController>();
		deathController = GetComponent<EnemyDeathController>();

		lookState = new EnemyLookState(this);
		shootState = new EnemyShootState(this);
		deathState = new EnemyDeathState(this);
		stateMachine = new StateMachine(lookState);

		currentHealth = maxHealth;
	}


	protected override void Update() {
		if (currentHealth <= 0) {
			stateMachine.TransitionTo(deathState);
		}
		else if (stateMachine.currentState == lookState) {
			if (!attackController.isOnCooldown) {
				stateMachine.TransitionTo(shootState);
			}
		}
		else if (stateMachine.currentState == shootState) {
			stateMachine.TransitionTo(lookState);
		}
		base.Update();
	}
}
