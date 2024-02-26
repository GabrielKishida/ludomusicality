using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 1000f;
    public float maxSpeed = 15.0f; // Movement speed
    public float acceleration = 0.5f;
    public float dragCoefficient = 0.02f;
    public float minimumSpeed = 0.5f;
    public float gravity = 20.0f; // Gravity force

    private Vector2 currentSpeed = Vector2.zero;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction roll;
    private InputAction attack;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        roll = playerControls.Player.Roll;
        attack = playerControls.Player.Attack;
        move.Enable();
        roll.Enable();
        attack.Enable();

    }

    private void OnDisable()
    {
        move.Disable();
        roll.Disable();
        attack.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveCharacter();
        Attack();
    }

    private void MoveCharacter()
    {
        if (currentSpeed.magnitude < minimumSpeed)
        {
            currentSpeed = Vector2.zero;
        }
        else
        {
            currentSpeed = currentSpeed - currentSpeed * dragCoefficient;
        }
        Vector2 moveInput = move.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            currentSpeed += moveInput * acceleration;
            if (currentSpeed.magnitude > maxSpeed)
            {
                currentSpeed = Vector3.ClampMagnitude(currentSpeed, maxSpeed);
            }
        }
        moveDirection = new Vector3(currentSpeed.x, 0.0f, currentSpeed.y);
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        moveDirection.y -= gravity;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void Attack()
    {
        if (attack.ReadValue<float>() == 1)
        {
            Debug.Log("Attack");
        }
    }
}