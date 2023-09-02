
using System.Collections;
using UnityEngine;

public class GrenadeSpawner : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform spawnPoint;
    public float respawnDelay = 5f;
    private bool isSpawned = false;

    private GameObject currentGrenade;

    private Rigidbody rb;

    private void Start()
    {
        SpawnGrenade();

       
    }
    private void SpawnGrenade()
    {
        currentGrenade = Instantiate(grenadePrefab, spawnPoint.position, spawnPoint.rotation);
        isSpawned = true;
    }

    private IEnumerator RespawnGrenade()
    {
        yield return new WaitForSeconds(respawnDelay);

        if (!isSpawned)
        {
            SpawnGrenade();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {

            other.GetComponent<Rigidbody>().useGravity = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("grenade"))
        {
            isSpawned = false;
            StartCoroutine(RespawnGrenade());
        }
    }

}
