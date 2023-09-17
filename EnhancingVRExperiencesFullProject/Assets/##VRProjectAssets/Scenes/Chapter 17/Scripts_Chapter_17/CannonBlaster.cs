using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBlaster : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public GameObject impactPrefab;
    public float impactForce = 10f;
    public GameObject cannonNozzle;
    public AudioClip blastSound;
    private AudioSource cannonAudio;
    public bool cannonActive;

    private void Awake()
    {
        cannonAudio = GetComponent<AudioSource>();
    }

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, cannonNozzle.transform.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
        cannonAudio.PlayOneShot(blastSound);
        
    }


}
