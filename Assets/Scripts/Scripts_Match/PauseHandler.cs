using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    private bool paused = false;

    // Variablees to control the opacity of the pause menu.
    private Image pause_panel_renderer;
    private TextMeshProUGUI[] pause_text_renderers;

    void Start()
    {
        pause_panel_renderer = GetComponent<Image>();
        pause_text_renderers = GetComponentsInChildren<TextMeshProUGUI>(true);
    }

    void Update()
    {
        // Allows to pause while preventing pausing during a countdown.
        if (Input.GetKeyDown(KeyCode.Backspace) && Time.timeScale != 0)
        {
            pause();
        }
    }

    // Simulates a pause menu.
    public void pause()
    {
        paused = true;
        Time.timeScale = 0;
        pause_panel_renderer.color = new Color(0, 0, 0, 225);
        foreach (TextMeshProUGUI pause_text_renderer in pause_text_renderers)
        {
            pause_text_renderer.color = new Color(255, 255, 255, 255);
        }
    }
    public void resume()
    {
        if (paused)
        {
            paused = false;
            pause_panel_renderer.color = new Color(0, 0, 0, 0);
            foreach (TextMeshProUGUI pause_text_renderer in pause_text_renderers)
            {
                pause_text_renderer.color = new Color(255, 255, 255, 0);
            }

            // Plays the countdown.
            StartCoroutine(GetComponentInParent<MatchHandler>().matchCountdown());
        }
    }
    public void exit()
    {
        if (paused)
        {
            GetComponentInParent<SceneNavigation>().endMatch();
        }
    }
}
