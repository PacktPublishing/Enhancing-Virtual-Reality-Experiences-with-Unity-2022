using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBlaster : MonoBehaviour
{
    // The prefab of the projectile to be fired
    public GameObject projectilePrefab;
    // The speed at which the projectile travels
    public float projectileSpeed = 10f;
    // The prefab of the impact effect
    public GameObject impactPrefab;
    // The force applied to objects upon impact
    public float impactForce = 10f;
    // The position of the cannon's nozzle
    public GameObject cannonNozzle;
    // The sound played when firing the cannon
    public AudioClip blastSound;
    // The audio source component for playing sounds
    private AudioSource cannonAudio;
    // Flag to track if the cannon is active
    public bool cannonActive;

    private void Awake()
    {
        // Get the AudioSource component
        cannonAudio = GetComponent<AudioSource>();
    }

    public void FireProjectile()
    {
        // Instantiate the projectile at the cannon's nozzle position
        GameObject projectile = Instantiate(projectilePrefab, cannonNozzle.transform.position, transform.rotation);
        // Get the Rigidbody component of the projectile
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        // Set the velocity of the projectile to propel it forward
        rb.velocity = transform.forward * projectileSpeed;
        // Play the blast sound
        cannonAudio.PlayOneShot(blastSound);
    }
}
