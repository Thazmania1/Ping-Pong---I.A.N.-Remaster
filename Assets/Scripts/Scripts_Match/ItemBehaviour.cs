using UnityEngine;
using UnityEngine.InputSystem;

public class ItemBehaviour : MonoBehaviour
{
    // Tracks if the item has been claimed by a player.
    private bool obtained = false;

    // Keeps track of the action.
    private System.Action<InputAction.CallbackContext> item_use_handler;

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
        if
        (
            obtained &&
            gameObject.transform.parent.name.Split("_")[0].Equals(player) &&
            Time.timeScale != 0
        )
        {
            // Frees the slot for another item.
            GameObject player_object = transform.root.gameObject;
            player_object.GetComponent<PlayerControls>().toggleHasItem();
            PlayerInput player_input = player_object.GetComponent<PlayerInput>();
            if (player_object.name.Equals("Player1"))
            {
                InputAction action = player_input.actions.FindActionMap("Player1Controls").FindAction("Item");
                action.performed -= item_use_handler;
            }
            else
            {
                InputAction action = player_input.actions.FindActionMap("Player2Controls").FindAction("Item");
                action.performed -= item_use_handler;
            }
            Destroy(gameObject);
        }
    }

    // For special cases.
    public void obtainedItemSelfDestroy()
    {
        GameObject player_object = transform.root.gameObject;
        PlayerInput player_input = player_object.GetComponent<PlayerInput>();
        if (player_object.name.Equals("Player1"))
        {
            InputAction action = player_input.actions.FindActionMap("Player1Controls").FindAction("Item");
            action.performed -= item_use_handler;
        }
        else
        {
            InputAction action = player_input.actions.FindActionMap("Player2Controls").FindAction("Item");
            action.performed -= item_use_handler;
        }
        Destroy(gameObject);
    }

    // Checks what collided.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            obtained = true;

            // Makes sure the ball doesn't get stuck behind the players.
            Destroy(gameObject.GetComponent<Collider2D>());

            // Resets rotation.
            transform.rotation = Quaternion.identity;

            // Finds the backpack of the claiming player, and makes the item a child of it while mantaining proper scaling.
            GameObject player_backpack = GameObject.Find(collision.gameObject.GetComponent<BallBehaviour>().getLastHit() + "_Backpack");
            gameObject.transform.SetParent(player_backpack.transform, false);
            gameObject.transform.position = new Vector3(player_backpack.transform.position.x, player_backpack.transform.position.y, -1);
            gameObject.transform.localScale = new Vector3(1, 0.5f, 1);
            
            // Gracefully handles item replacement.
            GameObject player_object = transform.root.gameObject;
            PlayerControls player_controls = player_object.GetComponent<PlayerControls>();
            if (player_controls.getHasItem())
            {
                player_controls.replaceItem();
            }
            else
            {
                player_controls.toggleHasItem();
            }

            // Makes item usable for the corresponding player, and saves the action to be later handled properly.
            PlayerInput player_input = player_object.GetComponent<PlayerInput>();
            if (player_object.name.Equals("Player1"))
            {
                InputAction action = player_input.actions.FindActionMap("Player1Controls").FindAction("Item");
                item_use_handler = ctx => use("Player1");
                action.performed += item_use_handler;
            }
            else
            {
                InputAction action = player_input.actions.FindActionMap("Player2Controls").FindAction("Item");
                item_use_handler = ctx => use("Player2");
                action.performed += item_use_handler;
            }
        }
    }
}
