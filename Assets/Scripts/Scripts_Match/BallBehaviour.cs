using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    // Declares a class level ball physics variable.
    private Rigidbody2D ball_physics;

    // Defines the ball's speeds and directions.
    private float x_speed;
    private float y_speed;
    private int x_direction;
    private int y_direction;

    // References the match handler.
    GameObject match_handler;

    // Keeps track of the last player that hit the ball.
    private string last_hit = "";

    void Start()
    {
        ball_physics = GetComponent<Rigidbody2D>();

        match_handler = GameObject.Find("UI");

        x_speed = 5f;
        y_speed = 5f;
        x_direction = (Random.Range(0, 2) * 2) - 1;
        y_direction = (Random.Range(0, 2) * 2) - 1;
    }

    void Update()
    {
        // Dynamic speed calculation.
        ball_physics.linearVelocity = new Vector2(x_speed * x_direction, y_speed * y_direction);
    }

    // Checks what collided.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("VerticalBorder"))
        {
            // Reverses the vertical direction of the ball.
            y_direction *= -1;
        }
    }

    // Checks what was triggered.
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Contains all of the ball's hits logic.
        if (collision.CompareTag("BallCollision"))
        {
            // Gets the player's physics for further logic implementation.
            Rigidbody2D player_physics = collision.GetComponentInParent<Rigidbody2D>();

            last_hit = collision.name.Split("_")[0];

            // "Whiff" hit.
            if
            (
                collision.transform.parent.name.Equals("Player1") && gameObject.transform.position.x < collision.transform.position.x ||
                collision.transform.parent.name.Equals("Player2") && gameObject.transform.position.x > collision.transform.position.x
            )
            {
                y_direction *= -1;
                x_speed++;
                y_speed++;
            }

            // Succesful hit.
            else
            {
                x_direction *= -1;
                x_speed += 0.25f;
                
                // Regular top/bottom hit.
                if
                (
                    y_direction == 1 && gameObject.transform.position.y < collision.transform.position.y - 2 ||
                    y_direction == -1 && gameObject.transform.position.y > collision.transform.position.y + 2
                )
                {
                    y_direction *= -1;
                }

                // Reverse top/bottom hit. (Yes, this feature was not intentional.)
                else if
                (
                    y_direction == 1 && gameObject.transform.position.y > collision.transform.position.y + 2 ||
                    y_direction == -1 && gameObject.transform.position.y < collision.transform.position.y - 2
                )
                {
                    y_speed = y_speed <= 5 ? 5 : y_speed - 1;
                    x_speed = x_speed >= 10 ? 10 : x_speed + 2;
                }

                // Force transfer logic.
                if
                (
                    y_direction == 1 && player_physics.linearVelocityY > 0 ||
                    y_direction == -1 && player_physics.linearVelocityY < 0
                )
                {
                    y_speed = y_speed >= 10 ? 10 : y_speed + 1;
                }
                else if
                (
                    y_direction == 1 && player_physics.linearVelocityY < 0 ||
                    y_direction == -1 && player_physics.linearVelocityY > 0
                )
                {
                    y_direction *= -1;
                    y_speed = y_speed <= 5 ? 5 : y_speed - 1;
                    x_speed = x_speed <= 5 ? 5 : x_speed - 1;
                }
            }
        }

        // Updates the score, resets every object's position and randomizes its own initial physics.
        if (collision.CompareTag("Goal"))
        {
            GameObject scoring_player = transform.position.x > 0 ? GameObject.Find("Player1") : GameObject.Find("Player2");

            scoring_player.GetComponent<PlayerControls>().updateScore();
            match_handler.GetComponent<MatchHandler>().updateRemainingRounds();

            transform.position = new Vector3(0, 0, transform.position.z);
            x_speed = 5f;
            y_speed = 5f;
            x_direction = (Random.Range(0, 2) * 2) - 1;
            y_direction = (Random.Range(0, 2) * 2) - 1;

            GameObject player1 = GameObject.Find("Player1");
            GameObject player2 = GameObject.Find("Player2");
            player1.transform.position = new Vector3(player1.transform.position.x, 0, player1.transform.position.z);
            player2.transform.position = new Vector3(player2.transform.position.x, 0, player2.transform.position.z);

            last_hit = "";

            StartCoroutine(match_handler.GetComponent<MatchHandler>().matchCountdown());
        }
    }

    // Getters.
    public string getLastHit()
    {
        return last_hit;
    }
}
