using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // Declares a class level player physics variable.
    private Rigidbody2D player_physics;

    // Defines the player's speed.
    private float speed = 5f;

    void Start()
    {
        player_physics = GetComponent<Rigidbody2D>();

        // Makes players' restrictive colliders not be interactable with the ball.
        Collider2D player_collider = GetComponent<Collider2D>();
        Collider2D ball_collider = GameObject.Find("Ball").GetComponent<Collider2D>();
        // Physics2D.IgnoreCollision(player_collider, ball_collider, true);
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
        player_physics.linearVelocityY = input * speed;
    }
}
