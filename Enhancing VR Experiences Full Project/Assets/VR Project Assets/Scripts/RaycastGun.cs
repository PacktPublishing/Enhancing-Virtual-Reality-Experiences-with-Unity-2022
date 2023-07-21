using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[RequireComponent(typeof(LineRenderer))]
public class RaycastGun : MonoBehaviour
{
//tutorial - https://youtu.be/YjpKxjzwado

    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;
 public Transform firePoint;
public GameObject chargeProjectile;
public float chargeSpeed;
public float chargeTime;
public bool isCharging;


    LineRenderer laserLine;
    float fireTimer;
 
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }
 
    void Update()
        {
            if(chargeTime<2)//&&inputactionpress
                {
                    isCharging = true;
                    if(isCharging==true)
                    {
                        chargeTime+=Time.deltaTime *chargeSpeed;
                    }

                }

        if (isCharging)//inputactionrlease
        {
            fireTimer += Time.deltaTime;
            if (Input.GetButtonDown("Fire1") && fireTimer > fireRate)
            {
                fireTimer = 0;
                laserLine.SetPosition(0, laserOrigin.position);
                Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, gunRange))
                {
                    laserLine.SetPosition(1, hit.point);
                    Destroy(hit.transform.gameObject);
                }
                else
                {
                    laserLine.SetPosition(1, rayOrigin + (playerCamera.transform.forward * gunRange));
                }
                StartCoroutine(ShootLaser());
            }
            chargeTime = 0;
        }
        else if (chargeTime >= 2);//inputactionrelease
                {
                    ReleaseCharge();
                }
        }
 public void ReleaseCharge(){
    Instantiate(chargeProjectile,firePoint.position,firePoint.rotation);
    isCharging=false;
    chargeTime=0;
 }
    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}
