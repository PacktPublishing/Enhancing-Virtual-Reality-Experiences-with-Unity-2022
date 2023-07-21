using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class ProjectileProperties
{
    public string projectileName;
    public GameObject projectilePrefab;
    public GameObject impactPrefabs;
    public bool isProjectile;


    public bool rapidFireEnabled;
    public int rapidFireRate;
    public float rapidFireDelay = 0.1f;
}

public class CannonBlasterComplex : MonoBehaviour
{
    public List<ProjectileProperties> projectileProperties;
    public int currentProjectileIndex = 0;
    public GameObject cannonNozzle;
    private GameObject currentNonProjectile;
    public float speed = 10f;
    public AudioClip blastSound;
    private AudioSource cannonAudio;


    public bool cannonHeld;


 

    private void Awake()
    {
        cannonAudio = GetComponent<AudioSource>();
    }

    public void PressTrigger()
    {
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
        GameObject projectile = Instantiate(properties.projectilePrefab, cannonNozzle.transform.position, transform.rotation);
        cannonAudio.PlayOneShot(blastSound);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
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
        GameObject projectile = Instantiate(properties.projectilePrefab, cannonNozzle.transform.position, transform.rotation, cannonNozzle.transform);
        currentNonProjectile = projectile;
        projectile.SetActive(true);
    }

    public void ReleaseTrigger()
    {
        if (!projectileProperties[currentProjectileIndex].isProjectile)
        {
            Destroy(currentNonProjectile);
        }
    }



    public void NextProjectile()
    {
        if (cannonHeld == true)
        {
            currentProjectileIndex = (currentProjectileIndex + 1) % projectileProperties.Count;

        }

    }



    public void PreviousProjectile()
    {
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
        cannonHeld = true;

    }

    public void ReleaseCannon()
    {
        cannonHeld = false;
    }




    //public void PressTrigger()
    //{

    //    //ProjectileProperties properties = projectileProperties[currentProjectileIndex];



    //    //if (properties.isProjectile)
    //    //{
    //    //    GameObject projectile = Instantiate(projectileProperties[currentProjectileIndex].projectilePrefab, cannonNozzle.transform.position, transform.rotation);

    //    //    cannonAudio.PlayOneShot(blastSound);

    //    //    Rigidbody rb = projectile.GetComponent<Rigidbody>();
    //    //        rb.velocity = transform.forward * speed;

    //    //}
    //    //else
    //    //{
    //    //    GameObject projectile = Instantiate(projectileProperties[currentProjectileIndex].projectilePrefab, cannonNozzle.transform.position, transform.rotation,cannonNozzle.transform);
    //    //    currentNonProjectile = projectile;
    //    //    projectile.SetActive(true);
    //    //}

    //    ProjectileProperties properties = projectileProperties[currentProjectileIndex];

    //    if (properties.isProjectile)
    //    {
    //        if (properties.rapidFireEnabled)
    //        {
    //            // StartCoroutine(RapidFireProjectile(properties));
    //            StartCoroutine(RapidFireProjectile(properties, properties.rapidFireRate));

    //        }
    //        FireProjectile(properties);
    //    }
    //    else
    //    {
    //        NonProjectile(properties);
    //    }
    //}

    //private IEnumerator RapidFireProjectile(ProjectileProperties properties)
    //{
    //    FireProjectile(properties);
    //    yield return new WaitForSeconds(rapidFireDelay);
    //    FireProjectile(properties);
    //    yield return new WaitForSeconds(rapidFireDelay);
    //    FireProjectile(properties);

    //}
}
