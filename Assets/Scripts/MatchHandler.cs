using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// Allows to call MatchSetupData class.
using static MatchSetup;

public class MatchHandler : MonoBehaviour
{
    // Determines which players are AI or "human". [0] = player 1, [1] = player 2.
    private List<bool> players_ai = new List<bool> { false, true };

    // Determines the amount of rounds.
    private int rounds = 3;

    // Determines the path where the persistent data will be located.
    private string save_path = "";

    void Start()
    {
        // Applies path in Start since it's not allowed in class level.
        save_path = Path.Combine(Application.persistentDataPath, "matchsetup.dat");

        // Retrieves user's decisions and loads them.
        BinaryFormatter binary_formatter = new BinaryFormatter();
        FileStream action = new FileStream(save_path, FileMode.Open);
        MatchSetupData loaded_data = (MatchSetupData)binary_formatter.Deserialize(action);
        players_ai = loaded_data.players_ai;
        rounds = loaded_data.rounds;
    }

    void Update()
    {
        
    }


    // Getters.
    public bool getPlayers_ai(int player)
    {
        return players_ai[player];
    }

    public int getRounds()
    {
        return rounds;
    }
}
