using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed = 1000f;
    public float maxSpeed = 8.0f; // Movement speed
    public float acceleration = 1.0f;
    public float deacceleration = 0.9f;
    public float minimumSpeed = 0.5f;
    public float jumpSpeed = 8.0f; // Jump speed
    public float gravity = 20.0f; // Gravity force

    private Vector2 currentSpeed = Vector2.zero;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public InputAction playerInput;

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
            }


            if (currentSpeed.magnitude < minimumSpeed)
            {
                currentSpeed = Vector2.zero;
            }
            else
            {
                currentSpeed = currentSpeed * deacceleration;
            }

            Vector2 moveInput = playerInput.ReadValue<Vector2>();
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

        }
        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}