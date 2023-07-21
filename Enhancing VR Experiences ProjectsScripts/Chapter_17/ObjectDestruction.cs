using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{
    // The colliders affected by the explosion
    public Collider[] particles;
    // The radius of the explosion
    public float explosionRadius = 5f;
    // The force of the explosion
    public float explosionForce = 500f;
    // The delay time before destroying the object and its particles
    public float delayTime;
    // The center of the explosion
    public Transform explosionCenter;

    private void OnEnable()
    {
        // Loop through each collider in the particles array
        foreach (Collider collider in particles)
        {
            // Get the rigidbody component of the collider
            Rigidbody otherRigidbody = collider.GetComponent<Rigidbody>();

            // Check if the rigidbody exists
            if (otherRigidbody != null)
            {
                // Apply explosion force to the rigidbody
                otherRigidbody.AddExplosionForce(explosionForce, explosionCenter.transform.position, explosionRadius);
            }

            // Destroy the collider's game object after the specified delay time
            Destroy(collider.gameObject, delayTime);
        }

        // Destroy the object after the specified delay time
        Destroy(gameObject, delayTime);
    }
}
