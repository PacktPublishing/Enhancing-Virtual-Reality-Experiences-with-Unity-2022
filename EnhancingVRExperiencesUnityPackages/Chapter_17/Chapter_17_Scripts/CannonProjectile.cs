using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{
    // The prefab of the impact effect
    public GameObject impactPrefab;
    // The force applied upon impact
    public float impactForce = 10f;
    // The delay before destroying the projectile
    public float destroyDelay = 2f;

    private CannonBlaster cannonBlaster;

    private void Awake()
    {
        // Get the CannonBlaster component from the same game object
        cannonBlaster = GetComponent<CannonBlaster>();
        // Set the impact prefab and force from the CannonBlaster component
        impactPrefab = cannonBlaster.impactPrefab;
        impactForce = cannonBlaster.impactForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Get the Rigidbody component of the collided game object
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply a force to the collided game object in the direction of the projectile's forward vector
            rb.AddForce(transform.forward * impactForce, ForceMode.Impulse);
        }

        // Instantiate the impact effect at the collision point
        GameObject impact = Instantiate(impactPrefab, collision.contacts[0].point, Quaternion.identity);

        // Destroy the projectile and impact effect after a delay
        Destroy(this.gameObject, destroyDelay - 0.1f);
        Destroy(impact, destroyDelay + 0.2f);
    }
}
