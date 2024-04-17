using System.Collections;
using UnityEngine;

public class EnemyManager : CharacterManager {
	public EnemyAttackController attackController;

	[SerializeField] private MusicEventScriptableObject enemyAttackEvent;

	public IState lookState;
	public IState shootState;
	public IState deathState;

	[Header("Health")]
	[SerializeField] private float maxHealth;
	[SerializeField] private float currentHealth;

	[SerializeField] private float timeToDespawn = 1.0f;

	public void ShootEvent() {
		if (currentHealth > 0) { stateMachine.TransitionTo(shootState); }
	}

	public override void OnHurtboxHit(float damage, Vector3 knockback) {
		currentHealth -= damage;
		base.OnHurtboxHit(damage, knockback);
	}

	public void Die() {
		Destroy(gameObject, timeToDespawn);
	}


	protected override void Start() {
		base.Start();
		attackController = GetComponent<EnemyAttackController>();

		lookState = new EnemyLookState(this);
		shootState = new EnemyShootState(this);
		deathState = new EnemyDeathState(this);
		stateMachine = new StateMachine(lookState);

		currentHealth = maxHealth;

		enemyAttackEvent.musicEvent.AddListener(ShootEvent);
	}


	protected override void Update() {
		if (currentHealth <= 0) {
			stateMachine.TransitionTo(deathState);
		}
		base.Update();
	}
}
