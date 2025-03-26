using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MatchSetup : MonoBehaviour
{
    // Determines which players are AI or "human". [0] = player 1, [1] = player 2.
    private static List<bool> players_ai = new List<bool> { false, true };

    // Determines the amount of rounds.
    private static int rounds = 3;

    // Determines the path where the persistent data will be saved.
    private string save_path = "";

    // Compact persistent data.
    [System.Serializable]
    public class MatchSetupData
    {
        public List<bool> players_ai { get; private set; }
        public int rounds { get; private set; }

        public MatchSetupData(List<bool> players_ai, int rounds)
        {
            this.players_ai = players_ai;
            this.rounds = rounds;
        }
    }

    void Start()
    {
        // Applies path in Start since it's not allowed in class level.
        save_path = Path.Combine(Application.persistentDataPath, "matchsetup.dat");

        // Checks if the save file already exists.
        if (File.Exists(save_path))
        {
            // Retrieves user's decisions and loads them.
            BinaryFormatter binary_formatter = new BinaryFormatter();
            FileStream action = new FileStream(save_path, FileMode.Open);
            MatchSetupData loaded_data = (MatchSetupData)binary_formatter.Deserialize(action);

            // Each objects updates respectively.
            if (gameObject.name.Contains("Player"))
            {
                players_ai = loaded_data.players_ai;

                // Applies visual cues.
                string[] player_text = gameObject.name.Split("_");
                int player_number = int.Parse(player_text[0].Substring(player_text[0].Length - 1));
                playerAIVisualCues(player_number, player_text);
            }
            else if (gameObject.name.Equals("Objective_Text"))
            {
                rounds = loaded_data.rounds;
                TextMeshProUGUI objective_text = gameObject.GetComponent<TextMeshProUGUI>();
                if (rounds > 9)
                {
                    objective_text.SetText("Best of: \u221E");
                }
                else
                {
                    objective_text.SetText("Best of: " + rounds);
                }
            }
            action.Close();
        }
    }

    void Update()
    {
        
    }

    // Toggles between the player being AI and "Human".
    public void playerAIToggle()
    {
        // Splits the object's name.
        string[] player_text = gameObject.name.Split("_");

        // Gets the player's number.
        int player_number = int.Parse(player_text[0].Substring(player_text[0].Length - 1));

        // Toggles the player's AI state. (True or False)
        players_ai[player_number - 1] = !players_ai[player_number - 1];

        playerAIVisualCues(player_number, player_text);
    }

    public void playerAIVisualCues(int player_number, string[] player_text)
    {
        // Visual cues to the object's children to represent what the player is now.
        // "TextMeshPro" is legacy, "TextMeshProUGUI" is modern.
        Transform ai_object = transform.Find(player_text[0] + "_" + player_text[1] + "_AI");
        TextMeshProUGUI ai_object_text = ai_object.GetComponent<TextMeshProUGUI>();
        Transform human_object = transform.Find(player_text[0] + "_" + player_text[1] + "_Human");
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

    // Makes user's decisions persistent and calls the match to be started.
    public void MatchStart()
    {
        BinaryFormatter binary_formatter = new BinaryFormatter();
        FileStream action = new FileStream(save_path, FileMode.Create);

        binary_formatter.Serialize
        (
            action,
            new MatchSetupData
            (
                players_ai,
                rounds
            )
        );

        action.Close();
        
        SceneNavigation start_match = GetComponent<SceneNavigation>();
        start_match.startMatch();
    }
}
