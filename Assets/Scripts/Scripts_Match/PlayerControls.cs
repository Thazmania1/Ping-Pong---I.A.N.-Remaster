using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // Declares a class level player physics variable.
    private Rigidbody2D player_physics;

    // Defines the player's speed.
    private float speed = 4f;

    // Determines whether the player is AI or "Human".
    private bool ai;

    // Determines the player's score.
    private int score = 0;

    // Reference to the ball object.
    GameObject ball;

    void Start()
    {
        player_physics = GetComponent<Rigidbody2D>();

        // Each gets retrieves its respective role.
        GameObject match_handler = GameObject.Find("UI");
        ai = match_handler.GetComponent<MatchHandler>().getPlayers_ai(int.Parse(gameObject.name.Substring(gameObject.name.Length - 1)));

        // If the player results to be AI, get the ball's object reference.
        if (ai)
        {
            ball = GameObject.Find("Ball");
        }
    }

    void Update()
    {
        if (ai)
        {
            player_physics.linearVelocityY = ball.transform.position.y > gameObject.transform.position.y ? speed : -speed;
        }
    }

    // Executes when keys mapped for movement are pressed. ("Humans" only.)
    public void PlayerMove(InputAction.CallbackContext action)
    {
        if (!ai)
        {
            // Gets the positive/negative value.
            float input = action.ReadValue<float>();

            // Sets the vertical movement velocity.
            player_physics.linearVelocityY = input * speed;
        }
    }

    public void updateScore()
    {
        score++;
        gameObject.GetComponentInChildren<TextMeshPro>().text = score.ToString();
    }

    // Getters.
    public int getScore()
    {
        return score;
    }
}
