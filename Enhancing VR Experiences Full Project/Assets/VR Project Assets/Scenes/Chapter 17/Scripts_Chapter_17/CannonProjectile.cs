using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{

    public GameObject impactPrefab;
    public float impactForce = 10f;
    public float destroyDelay=2f;

    private CannonBlaster cannonBlaster;
    private void Awake()
    {
        cannonBlaster = GetComponent<CannonBlaster>();
        impactPrefab = cannonBlaster.impactPrefab;
        impactForce = cannonBlaster.impactForce;
    }
    private void OnCollisionEnter(Collision collision)
    {

        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * impactForce, ForceMode.Impulse);
        }

        GameObject impact = Instantiate(impactPrefab, collision.contacts[0].point, Quaternion.identity);

        Destroy(this.gameObject, destroyDelay-.1f);
        Destroy(impact, destroyDelay+.2f); 
    }
}
