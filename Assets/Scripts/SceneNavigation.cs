using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    void Start()
    {
        
    }

    // Finds which scene is currently loaded and executes the scene's respective function.
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            titleNavigation();
        }
        else if (SceneManager.GetActiveScene().name == "Menu")
        {
            // menuNavigation();
        }
        else if (SceneManager.GetActiveScene().name == "Match")
        {
            matchNavigation();
        }
    }

    void titleNavigation()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void menuNavigation()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("Title");
        }
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Match");
        }
    }

    void matchNavigation()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
