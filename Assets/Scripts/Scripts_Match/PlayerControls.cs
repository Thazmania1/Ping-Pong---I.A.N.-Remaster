using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    // Declares a class level player physics variable.
    private Rigidbody2D player_physics;

    // Reference to the player's backpack.
    private Transform player_backpack;

    // Defines the player's speed.
    private float speed = 4f;

    // Determines whether the player is AI or "Human".
    private bool ai;

    // Determines the player's score.
    private int score = 0;

    // Tracks if the player has an item.
    private bool has_item = false;

    // Reference to the ball object.
    GameObject ball;

    void Start()
    {
        player_physics = GetComponent<Rigidbody2D>();
        player_backpack = gameObject.transform.GetChild(0);

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

    public void replaceItem()
    {
        player_backpack.GetChild(0).GetComponent<ItemBehaviour>().obtainedItemSelfDestroy();
    }

    // Getters and setters.
    public int getScore()
    {
        return score;
    }

    public bool getHasItem()
    {
        return has_item;
    }

    public void toggleHasItem()
    {
        has_item = !has_item;
    }
}
