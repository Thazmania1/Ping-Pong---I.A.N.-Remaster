using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // Declares a class level player physics variable.

    private Rigidbody2D physics;

    // Defines the player's speed.
    private float speed = 5f;
    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    // Executes when keys mapped for movement are pressed. ("Humans" only.)
    public void PlayerMove(InputAction.CallbackContext action)
    {
        // Gets the positive/negative value.
        float input = action.ReadValue<float>();
        
        // Sets the vertical movement velocity.
        physics.linearVelocityY = input * speed;
    }
}
