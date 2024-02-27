using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 1000f;
    public float maxSpeed = 15.0f;
    public float acceleration = 0.5f;
    public float dragCoefficient = 0.02f;
    public float minimumSpeed = 0.5f;
    public float gravity = 20.0f;
    public float attackDuration = 0.2f;
    public float attackCooldown = 0.5f;


    public GameObject hitbox;
    private CharacterController controller;
    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction roll;
    private InputAction attack;
    private InputAction mouseDirection;


    private Vector2 currentSpeed = Vector2.zero;
    private Vector3 moveDirection = Vector3.zero;
    private bool isAttacking = false;
    private bool isOnCooldown = false;
    private Vector2 attackDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        roll = playerControls.Player.Roll;
        attack = playerControls.Player.Attack;
        mouseDirection = playerControls.Player.MouseDirection;
        move.Enable();
        roll.Enable();
        attack.Enable();
        mouseDirection.Enable();

    }

    private void OnDisable()
    {
        move.Disable();
        roll.Disable();
        attack.Disable();
        mouseDirection.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        hitbox.SetActive(false);
    }

    void Update()
    {
        MoveCharacter();
        Attack();
    }

    private void ApplyDrag()
    {
        if (currentSpeed.magnitude < minimumSpeed)
        {
            currentSpeed = Vector2.zero;
        }
        else
        {
            currentSpeed = currentSpeed - currentSpeed * dragCoefficient;
        }
    }

    private Vector3 GetMoveDirection()
    {
        Vector2 moveInput = isAttacking ? Vector2.zero : move.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            currentSpeed += moveInput * acceleration;
            if (currentSpeed.magnitude > maxSpeed)
            {
                currentSpeed = Vector3.ClampMagnitude(currentSpeed, maxSpeed);
            }
        }
        return new Vector3(currentSpeed.x, 0.0f, currentSpeed.y);
    }

    private void RotateTowards(Vector3 direction, float rotateSpeed)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private void MoveCharacter()
    {
        ApplyDrag();
        moveDirection = GetMoveDirection();
        if (isAttacking)
            RotateTowards(new Vector3(attackDirection.x, 0.0f, attackDirection.y), rotationSpeed * 2);

        else if (moveDirection != Vector3.zero)
            RotateTowards(moveDirection, rotationSpeed);

        moveDirection.y -= gravity;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Attack()
    {
        bool canAttack = !isAttacking && !isOnCooldown;
        if (attack.ReadValue<float>() == 1 && canAttack)
        {
            StartCoroutine(AttackCoroutine());
        }

    }

    private IEnumerator AttackCoroutine()
    {
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

    private IEnumerator AttackCooldownCoroutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }
}