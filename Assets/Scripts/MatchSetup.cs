using TMPro;
using UnityEngine;

public class MatchSetup : MonoBehaviour
{
    // Determines which players are AI or "human". [0] = player 1, [1] = player 2.
    private static bool[] players_ai = new bool[] { false, true };

    // Determines the amount of rounds.
    private static int rounds = 3;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // Toggles between the player being AI and "Human".
    public void playerAIToggle()
    {
        // Splits the object's name.
        string[] player_text = gameObject.name.Split("_");
        string player_prefix = player_text[0];

        // Gets the player's number.
        int player_number = int.Parse(player_prefix.Substring(player_prefix.Length - 1));

        // Toggles the player's AI state. (True or False)
        players_ai[player_number - 1] = !players_ai[player_number - 1];

        // Visual cues to the object's children to represent what the player is now.
        // "TextMeshPro" is legacy, "TextMeshProUGUI" is modern.
        Transform ai_object = transform.Find(player_prefix + "_" + player_text[1] + "_AI");
        TextMeshProUGUI ai_object_text = ai_object.GetComponent<TextMeshProUGUI>();
        Transform human_object = transform.Find(player_prefix + "_" + player_text[1] + "_Human");
        TextMeshProUGUI human_object_text = human_object.GetComponent<TextMeshProUGUI>();
        if (players_ai[player_number - 1] == true)
        {
            ai_object_text.color = new Color32(255, 255, 0, 255);
            human_object_text.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            ai_object_text.color = new Color32(255, 255, 255, 255);
            human_object_text.color = new Color32(255, 255, 0, 255);
        }
    }

    // Dynamically changes the match's rounds.
    // The clicked button passes its name to the function.
    public void changeRounds(string button_name)
    {
        rounds += button_name.Split('_')[1].Equals("Up") ? 2 : -2;


        // Updates rounds visually. A match must have at least one round, and if the number of rounds is higher than 9, then the match is infinite.
        TextMeshProUGUI objective_text = gameObject.GetComponent<TextMeshProUGUI>();
        if (rounds <= 9)
        {
            rounds = rounds < 1 ? 1 : rounds;
            objective_text.SetText("Best of: " + rounds);
        }
        else
        {
            rounds = rounds > 11 ? 11 : rounds;
            objective_text.SetText("Best of: \u221E");
        }
    }
}
