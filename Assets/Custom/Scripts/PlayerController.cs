using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f; // Movement speed
    public float jumpSpeed = 8.0f; // Jump speed
    public float gravity = 20.0f; // Gravity force

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public EventManager eventManager;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        eventManager.onJumpPressed.AddListener(HandleJump);
        eventManager.onHorizontalMove.AddListener(HandleHorizontalMove);
    }


    void Update()
    {


        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }
    void HandleJump()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        Debug.Log("Space key pressed by player!");
    }

    void HandleHorizontalMove(Vector2 direction)
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(direction.x, 0.0f, direction.y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

        }
    }
}