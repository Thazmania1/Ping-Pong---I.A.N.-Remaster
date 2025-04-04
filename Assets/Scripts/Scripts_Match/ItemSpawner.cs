using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Dragable Unity editor field to reference a prefab.
    [SerializeField]
    private GameObject item_prefab;

    // Keeps track of time passed to properly spawn items.
    float time_passed = 0;

    void Start()
    {
        
    }

    void Update()
    {
        // Spawns an item in a random position every 20 seconds
        time_passed += Time.deltaTime;
        if (time_passed >= 20f)
        {
            Vector3 random_position = new Vector3(Random.Range(-3f, 4f), Random.Range(-4f, 5f), 0f);

            // Makes the item a child of this object.
            GameObject new_item = Instantiate(item_prefab, random_position, Quaternion.identity);
            new_item.transform.SetParent(transform, true);
            time_passed = 0f;
        }
    }

    //  Goal item spawn cooldown.
    public void goalCooldown()
    {
        time_passed = 0;
    }

    // Unobtained items get destroyed when a goal occurs.
    public void lostItems()
    {
        foreach (Transform item in gameObject.transform)
        {
            Destroy(item.gameObject);
        }
    }
}
