using UnityEngine;

public class Hurtbox : MonoBehaviour {

    public delegate void DamageHandler(int damage);
    public DamageHandler OnTakeEnemyDamage;
    public DamageHandler OnTakePlayerDamage;

    private void Start() {
    }

    private void OnTriggerEnter(Collider col) {
    }
}
