using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

// Allows to call MatchSetupData class.
using static MatchSetup;

public class MatchHandler : MonoBehaviour
{
    // Determines which players are AI or "human". [0] = player 1, [1] = player 2.
    private List<bool> players_ai;

    // Determines the amount of rounds.
    private int rounds;

    // Determines the path where the persistent data will be located.
    private string save_path = "";

    // Variable to store the countdown text reference.
    TextMeshProUGUI countdown_text;

    void Awake()
    {
        // Applies path in Start since it's not allowed in class level.
        save_path = Path.Combine(Application.persistentDataPath, "matchsetup.dat");

        // Retrieves user's decisions and loads them.
        BinaryFormatter binary_formatter = new BinaryFormatter();
        FileStream action = new FileStream(save_path, FileMode.Open);
        MatchSetupData loaded_data = (MatchSetupData)binary_formatter.Deserialize(action);
        players_ai = loaded_data.players_ai;
        rounds = loaded_data.rounds;

        // Closes the file for error prevention.
        action.Close();

        // Gets the countdown text component.
        Transform countdown_object = transform.Find("Countdown_Text");
        countdown_text = countdown_object.GetComponent<TextMeshProUGUI>();

        StartCoroutine(matchCountdown());
    }

    void Update()
    {
        
    }

    // When there's no more rounds, the game ends.
    public void updateRemainingRounds()
    {
        rounds--;
        if (rounds == 0)
        {
            gameObject.GetComponent<SceneNavigation>().endMatch();
        }
    }

    // Time.timeScale is literally the speed of time.
    public IEnumerator matchCountdown()
    {
        countdown_text.color = new Color32(255, 255, 255, 255); // Makes countdown visible.
        Time.timeScale = 0;

        // 3 Second countdown.
        float elapsed_time = 0f;
        int ui_time = 3;
        while (elapsed_time < 3)
        {
            elapsed_time += Time.unscaledDeltaTime; // Tracks real-world time, based on time elapsed every frame.
            countdown_text.text = (ui_time - (int)elapsed_time).ToString(); // Truncates elapsed time to get a readable number.
            yield return null; // Waits until next frame.
        }

        Time.timeScale = 1;
        countdown_text.color = new Color32(255, 255, 255, 0); // Makes countdown invisible.
    }

    // Getters.
    public bool getPlayers_ai(int player)
    {
        return players_ai[player - 1];
    }

    public int getRounds()
    {
        return rounds;
    }
}
