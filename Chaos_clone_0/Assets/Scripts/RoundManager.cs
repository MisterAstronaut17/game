using Mirror;
using UnityEngine;
using System.Collections;

public class RoundManager : NetworkBehaviour
{
    [SyncVar]
    private int roundTimer;

    [SyncVar]
    private int teamAScore;

    [SyncVar]
    private int teamBScore;

    private bool roundActive = false; // Track if a round is currently active

    // Method to start the round timer (only executed by the server)
    [Server]
    public void StartRoundTimer(int initialTime)
    {
        roundTimer = initialTime;  // Set the initial timer value (e.g., 30 seconds)
        roundActive = true;  // Mark the round as active
        StartCoroutine(RoundTimerCountdown());  // Start countdown
    }

    // Coroutine to handle the round timer countdown
    private IEnumerator RoundTimerCountdown()
    {
        while (roundTimer > 0 && roundActive)
        {
            yield return new WaitForSeconds(1f);  // Wait 1 second
            roundTimer--;  // Decrease the timer by 1 second

            // Optionally: You can add logic to send notifications when the timer reaches certain values (e.g., 10 seconds left)
        }

        if (roundTimer <= 0)
        {
            EndRound();  // Call method to end the round when the timer reaches zero
        }
    }

    // Method to stop the round timer
    [Server]
    public void StopRoundTimer()
    {
        StopCoroutine(RoundTimerCountdown());  // Stop the countdown coroutine
        roundActive = false;  // Mark the round as inactive
    }

    // End round logic (e.g., evaluate scores, reset values)
    [Server]
    private void EndRound()
    {
        roundActive = false;  // Stop the round

        // Determine winner based on remaining players
        // Here you can implement logic for tracking which team has players alive

        // Example: Award points to the winning team
        if (DetermineWinningTeam() == "TeamA")
        {
            teamAScore++;
        }
        else if (DetermineWinningTeam() == "TeamB")
        {
            teamBScore++;
        }

        Debug.Log("Round has ended.");

        // Transition to customization phase
        StartCustomizationPhase();
    }

    // Method to determine the winning team (this is placeholder logic, adjust to your needs)
    [Server]
    private string DetermineWinningTeam()
    {
        // Placeholder logic: For now, return a random winner
        return Random.Range(0, 2) == 0 ? "TeamA" : "TeamB";
    }

    // Start the customization phase (after the round ends)
    [Server]
    private void StartCustomizationPhase()
    {
        Debug.Log("Customization phase started.");
        // Start customization countdown (20-30 seconds)
        StartCoroutine(CustomizationTimer(20));  // Pass the desired customization phase duration
    }

    // Customization phase timer
    private IEnumerator CustomizationTimer(int duration)
    {
        while (duration > 0)
        {
            yield return new WaitForSeconds(1f);
            duration--;
        }

        // After customization, start the next round
        StartRoundTimer(30);  // Adjust round duration as needed
    }

    // Optional: Client-side method to display the remaining time (synchronized through SyncVar)
    [ClientRpc]
    public void RpcDisplayTime()
    {
        Debug.Log("Time left: " + roundTimer);
    }

    void Update()
    {
        if (isServer)
        {
            // Optionally: Call RpcDisplayTime() to update clients with the current timer value
        }
    }
}
