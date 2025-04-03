using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{


    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, Time.time * 50f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
