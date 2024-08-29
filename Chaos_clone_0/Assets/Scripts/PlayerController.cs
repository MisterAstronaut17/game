
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    [SyncVar]
    public int health;

    [SyncVar]
    public string playerName;

    // Other SyncVars for attributes, abilities, etc.

    // Method to handle taking damage
    public void TakeDamage(int damage)
    {
        if (!isServer) return;  // Only the server should modify health

        health -= damage;
        if (health <= 0)
        {
            // Handle player death, e.g., respawn or eliminate
            Debug.Log(playerName + " has been defeated.");
        }
    }

    public void SpawnProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        NetworkServer.Spawn(projectile);
    }

    public void DestroyProjectile(GameObject projectile)
    {
        NetworkServer.Destroy(projectile);
    }

}
