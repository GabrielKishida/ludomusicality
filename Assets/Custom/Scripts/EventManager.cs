
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class EventManager : MonoBehaviour
{
    public UnityEvent onJumpPressed;
    public UnityEvent<Vector2> onHorizontalMove;
    public InputAction playerInput;

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onJumpPressed.Invoke();
        }

        Vector2 horizontalMove = playerInput.ReadValue<Vector2>();
        if (horizontalMove != Vector2.zero)
        {
            onHorizontalMove.Invoke(horizontalMove);
        }
    }
}
