using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public EventManager eventManager;
    void Start()
    {
        eventManager.onSpacePressed.AddListener(HandleSpacePress);
    }

    void HandleSpacePress()
    {
        // Code to execute when Space key is pressed
        Debug.Log("Space key pressed by player!");
    }
}