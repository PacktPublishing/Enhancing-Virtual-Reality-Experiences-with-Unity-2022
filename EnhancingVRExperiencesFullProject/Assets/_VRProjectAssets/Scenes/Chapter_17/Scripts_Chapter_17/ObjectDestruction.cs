using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{

    public Collider[] particles;
    public float explosionRadius = 5f;
    public float explosionForce = 500f;
    public float delayTime;
    public Transform explosionCenter;


    private void OnEnable()
    {
        //particles = Physics.OverlapSphere(transform.position, explosionRadius); This will apply to any thing within the radius
        foreach (Collider collider in particles)
        {
            Rigidbody otherRigidbody = collider.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                otherRigidbody.AddExplosionForce(explosionForce, explosionCenter.transform.position, explosionRadius);
            }
            Destroy(collider.gameObject,delayTime);
        }
        Destroy(gameObject, delayTime);
    }
}
