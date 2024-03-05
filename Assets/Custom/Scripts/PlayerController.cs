using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseCharacter {
    public float attackDuration = 0.2f;
    public float attackCooldown = 0.5f;

    public GameObject hitbox;
    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction roll;
    private InputAction attack;
    private InputAction mouseDirection;

    private bool isAttacking = false;
    private bool isOnCooldown = false;
    private Vector2 attackDirection = Vector2.zero;

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


    private void CheckAttack() {
        bool canAttack = !isAttacking && !isOnCooldown;
        if (attack.ReadValue<float>() == 1 && canAttack) {
            StartCoroutine(AttackCoroutine());
        }

    }

    private IEnumerator AttackCoroutine() {
        Vector2 mouseCoordinates = mouseDirection.ReadValue<Vector2>();
        Vector2 characterCoordinates = Camera.main.WorldToScreenPoint(transform.position);

        isAttacking = true;
        hitbox.SetActive(true);
        attackDirection = mouseCoordinates - characterCoordinates;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        hitbox.SetActive(false);
        StartCoroutine(AttackCooldownCoroutine());
    }

    private IEnumerator AttackCooldownCoroutine() {
        isOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }

    protected override void Start() {
        base.Start();
        hitbox.SetActive(false);
    }

    protected override void Update() {
        Vector2 moveInput = isAttacking ? Vector2.zero : move.ReadValue<Vector2>();
        CharacterMovement(moveInput);
        CheckAttack();
        if (isAttacking) {
            FastRotateTowards(new Vector3(attackDirection.x, 0.0f, attackDirection.y));
        }

        base.Update();
    }
}