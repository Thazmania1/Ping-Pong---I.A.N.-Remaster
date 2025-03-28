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

    void Start()
    {
        ball_physics = GetComponent<Rigidbody2D>();

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks what collided.
        if (collision.gameObject.CompareTag("VerticalBorder"))
        {
            // Reverses the vertical direction of the ball.
            y_direction *= -1;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Checks what was triggered.
        if (collision.CompareTag("BallCollision"))
        {
            // "Whiff" logic.
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
            else
            {
                x_direction *= -1;

                // Top/Bottom hit logic.
                if
                (
                    gameObject.transform.position.y > collision.transform.position.y + 2 ||
                    gameObject.transform.position.y < collision.transform.position.y - 2
                )
                {
                    y_direction *= -1;
                }
            }
        }
        else if (collision.CompareTag("Goal"))
        {
            // Resets the ball's position and randomizes its physics.
            transform.position = new Vector3(0, 0, transform.position.z);
            x_speed = 5f;
            y_speed = 5f;
            x_direction = (Random.Range(0, 2) * 2) - 1;
            y_direction = (Random.Range(0, 2) * 2) - 1;
        }
    }
}
