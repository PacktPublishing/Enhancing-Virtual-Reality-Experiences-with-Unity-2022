using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectileComplex : MonoBehaviour
{
    // The prefab of the impact effect
    public GameObject impactPrefab;
    // The force applied upon impact
    public float impactForce = 10f;
    // Determines if the projectile should be destroyed
    public bool destroy;
    // The tag used to identify objects that can be destroyed
    [TagSelector]
    public string destructionTag;
    // The delay before destroying the projectile
    public float destroyDelay = 2f;
    // The damage points inflicted by the projectile
    public float projectileDamagePoints;

    private void OnCollisionEnter(Collision collision)
    {
        // Instantiate the impact effect at the collision point
        GameObject impact = Instantiate(impactPrefab, collision.contacts[0].point, Quaternion.identity);

        if (collision.gameObject.CompareTag(destructionTag))
        {
            // If the collided object has the specified destruction tag, perform destruction logic
            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            // Instantiate destruction effect if desired
            // Instantiate(destructionPrefab, contact.point, rotation);

            // Attempt to damage the collided object if it has an ObjectHealth component
            ObjectHealth health = collision.transform.GetComponent<ObjectHealth>();
            if (health != null)
            {
                health.TakeDamage(projectileDamagePoints);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
        else
        {
            // If the collided object does not have the destruction tag, apply impact force
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * impactForce, ForceMode.Impulse);
            }
        }

        // Destroy the impact effect and the projectile after a delay
        Destroy(impact, destroyDelay);
        if (destroy)
        {
            Destroy(this.gameObject, destroyDelay - 2f);
        }
    }
}
