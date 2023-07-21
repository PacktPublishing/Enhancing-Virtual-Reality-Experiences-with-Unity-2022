using System.Collections;
using UnityEngine;

public class GrenadeSpawner : MonoBehaviour
{
    // The prefab of the grenade to spawn
    public GameObject grenadePrefab;
    // The spawn point of the grenade
    public Transform spawnPoint;
    // The delay before respawning a grenade
    public float respawnDelay = 5f;
    // Flag to track if a grenade is currently spawned
    private bool isSpawned = false;
    // Reference to the currently spawned grenade
    private GameObject currentGrenade;

    private void Start()
    {
        // Spawn the initial grenade
        SpawnGrenade();
    }

    private void SpawnGrenade()
    {
        // Instantiate the grenade prefab at the spawn point
        currentGrenade = Instantiate(grenadePrefab, spawnPoint.position, spawnPoint.rotation);
        // Set the spawned flag to true
        isSpawned = true;
    }

    private IEnumerator RespawnGrenade()
    {
        // Wait for the specified respawn delay
        yield return new WaitForSeconds(respawnDelay);

        // If no grenade is currently spawned, spawn a new one
        if (!isSpawned)
        {
            SpawnGrenade();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider belongs to a grenade
        if (other.gameObject.CompareTag("grenade"))
        {
            // Set the spawned flag to false
            isSpawned = false;
            // Start the coroutine to respawn the grenade
            StartCoroutine(RespawnGrenade());
        }
    }
}
