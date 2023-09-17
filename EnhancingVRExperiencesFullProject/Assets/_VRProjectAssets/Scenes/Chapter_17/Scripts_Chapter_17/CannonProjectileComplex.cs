using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectileComplex : MonoBehaviour
{

    public GameObject impactPrefab;
    public float impactForce = 10f;
    public bool destroy;
    [TagSelector]
    public string destructionTag;
    //public GameObject destructionPrefab;
    public float destroyDelay = 2f;
    public float projectileDamagePoints;

    private void OnCollisionEnter(Collision collision)
    {


            GameObject impact = Instantiate(impactPrefab, collision.contacts[0].point, Quaternion.identity);
        if (collision.gameObject.CompareTag(destructionTag))
        {

            ContactPoint contact = collision.contacts[0];
            Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
           // Instantiate(impactPrefab, contact.point, rotation);

            ObjectHealth health = collision.transform.GetComponent<ObjectHealth>();
            if (health != null)
            {
                health.TakeDamage(projectileDamagePoints);
            }
            else
            {
                Destroy(collision.gameObject);
            }
            
            //Destroy(destructionPrefab);
        }
        else
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * impactForce, ForceMode.Impulse);
            }
        }
    
            
        Destroy(impact, destroyDelay);
        Destroy(this.gameObject, destroyDelay-2f);
    }
}
