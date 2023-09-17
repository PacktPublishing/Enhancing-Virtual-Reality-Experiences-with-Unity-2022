using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class ProjectileProperties
{
    // The name of the projectile
    public string projectileName;
    // The prefab of the projectile
    public GameObject projectilePrefab;
    // The prefab of the impact effect
    public GameObject impactPrefabs;
    // Flag indicating if the object is a projectile or not
    public bool isProjectile;
    // Flag indicating if rapid fire is enabled
    public bool rapidFireEnabled;
    // The rate at which rapid fire projectiles are fired
    public int rapidFireRate;
    // The delay between each rapid fire projectile
    public float rapidFireDelay = 0.1f;
}

public class CannonBlasterComplex : MonoBehaviour
{
    // The list of projectile properties
    public List<ProjectileProperties> projectileProperties;
    // The index of the current projectile
    public int currentProjectileIndex = 0;
    // The position of the cannon's nozzle
    public GameObject cannonNozzle;
    // The current non-projectile object
    private GameObject currentNonProjectile;
    // The speed at which projectiles are fired
    public float speed = 10f;
    // The sound played when firing the cannon
    public AudioClip blastSound;
    // The audio source component for playing sounds
    private AudioSource cannonAudio;
    // Flag indicating if the cannon is held
    public bool cannonHeld;

    private void Awake()
    {
        // Get the AudioSource component
        cannonAudio = GetComponent<AudioSource>();
    }

    public void PressTrigger()
    {
        // Get the properties of the current projectile
        ProjectileProperties properties = projectileProperties[currentProjectileIndex];

        if (properties.isProjectile)
        {
            if (properties.rapidFireEnabled)
            {
                StartCoroutine(RapidFireProjectile(properties, properties.rapidFireRate));
            }
            FireProjectile(properties);
        }
        else
        {
            NonProjectile(properties);
        }
    }

    private void FireProjectile(ProjectileProperties properties)
    {
        // Instantiate the projectile at the cannon's nozzle position
        GameObject projectile = Instantiate(properties.projectilePrefab, cannonNozzle.transform.position, transform.rotation);
        // Play the blast sound
        cannonAudio.PlayOneShot(blastSound);
        // Get the Rigidbody component of the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        // Set the velocity of the projectile to propel it forward
        rb.velocity = transform.forward * speed;
    }

    private IEnumerator RapidFireProjectile(ProjectileProperties properties, int numberOfTimes)
    {
        for (int i = 0; i < numberOfTimes; i++)
        {
            FireProjectile(properties);
            yield return new WaitForSeconds(properties.rapidFireDelay);
        }
    }

    private void NonProjectile(ProjectileProperties properties)
    {
        // Instantiate the non-projectile object at the cannon's nozzle position
        GameObject projectile = Instantiate(properties.projectilePrefab, cannonNozzle.transform.position, transform.rotation, cannonNozzle.transform);
        currentNonProjectile = projectile;
        projectile.SetActive(true);
    }

    public void ReleaseTrigger()
    {
        // Destroy the current non-projectile object
        if (!projectileProperties[currentProjectileIndex].isProjectile)
        {
            Destroy(currentNonProjectile);
        }
    }

    public void NextProjectile()
    {
        // Switch to the next projectile in the list
        if (cannonHeld == true)
        {
            currentProjectileIndex = (currentProjectileIndex + 1) % projectileProperties.Count;
        }
    }

    public void PreviousProjectile()
    {
        // Switch to the previous projectile in the list
        if (cannonHeld == true)
        {
            currentProjectileIndex--;
            if (currentProjectileIndex < 0)
            {
                currentProjectileIndex = projectileProperties.Count - 1;
            }
        }
    }

    public void HoldCannon()
    {
        // Set the cannon as held
        cannonHeld = true;
    }

    public void ReleaseCannon()
    {
        // Set the cannon as released
        cannonHeld = false;
    }
}
