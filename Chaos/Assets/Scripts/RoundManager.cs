using UnityEngine;
using Mirror;

public class RoundManager : NetworkBehaviour
{
    [SyncVar]
    private int roundTimer;

    [SyncVar]
    private int teamAScore;

    [SyncVar]
    private int teamBScore;

    // Methods to manage the round timer, scores, and other game state
    public void StartNewRound()
    {
        // Logic to reset the round and scores
        roundTimer = 60; // Example: 60 seconds per round
        // Additional logic for starting a new round
    }

    [Server]
    public void UpdateScores(int teamAScoreChange, int teamBScoreChange)
    {
        teamAScore += teamAScoreChange;
        teamBScore += teamBScoreChange;
        // Additional logic to handle score updates
    }

    // Other round-related methods...
}
