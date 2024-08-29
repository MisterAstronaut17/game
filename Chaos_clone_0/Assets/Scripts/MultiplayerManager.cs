using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : NetworkManager
{
    // custom logic for your multiplayer game will go here

    // This method is called when a player connects to the server
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        // Create the player object from the player prefab
        GameObject player = Instantiate(playerPrefab);

        // Assign the player to a team or set other initial player data
        AssignPlayerToTeam(player);

        // Add the player to the game
        NetworkServer.AddPlayerForConnection(conn, player);
    }

    // Custom method to assign the player to a team
    private void AssignPlayerToTeam(GameObject player)
    {
        // Implement your team assignment logic here
        // Example: assign the player to the team with the fewest members
        // You could also store team data and assign accordingly
    }

    // This method is called when a player disconnects from the server
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        // Perform any necessary cleanup when a player disconnects

        // Call the base functionality to handle the rest
        base.OnServerDisconnect(conn);
    }
}
