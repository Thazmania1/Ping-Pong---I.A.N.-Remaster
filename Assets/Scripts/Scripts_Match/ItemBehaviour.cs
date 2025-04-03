using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    // Tracks if it's been claimed by a player.
    private bool obtained = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (!obtained)
        {
            // Spins 50 degrees per second.
            transform.rotation = Quaternion.Euler(0, 0, Time.time * 50f);
        }
    }

    // Simulates the use of the item.
    public void use(string player)
    {
        if (obtained && gameObject.transform.parent.name.Split("_")[0].Equals(player) && Time.timeScale != 0)
        {
            Destroy(gameObject);
        }
    }

    // Checks what collided.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            obtained = true;
            transform.rotation = Quaternion.identity; // Resets rotation.

            // Finds the backpack of the claiming player, and makes the item a child of it while mantaining proper scaling.
            GameObject player_backpack = GameObject.Find(collision.gameObject.GetComponent<BallBehaviour>().getLastHit() + "_Backpack");
            gameObject.transform.SetParent(player_backpack.transform, false);
            gameObject.transform.position = new Vector3(player_backpack.transform.position.x, player_backpack.transform.position.y, -1);
            gameObject.transform.localScale = new Vector3(1, 0.5f, 1);

            // Makes sure the ball doesn't get stuck behind the players.
            Destroy(gameObject.GetComponent<Collider2D>());
        }
    }
}
