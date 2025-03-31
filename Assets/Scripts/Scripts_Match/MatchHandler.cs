using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    }

    void Update()
    {
        
    }

    public void updateRemainingRounds()
    {
        rounds--;
        if (rounds == 0)
        {
            gameObject.GetComponent<SceneNavigation>().endMatch();
        }
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
