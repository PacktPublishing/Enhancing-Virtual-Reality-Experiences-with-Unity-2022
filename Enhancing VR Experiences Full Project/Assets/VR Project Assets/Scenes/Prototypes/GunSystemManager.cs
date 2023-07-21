using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class GunSystemManager : MonoBehaviour
{
    // Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    // Bools 
    bool shooting, readyToShoot, reloading;

    // Reference
    public Transform attackPoint;
    public LayerMask whatIsEnemy;

    // Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    //public CamShake camShake;
    //public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    private XRGrabInteractable grabbable;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

         grabbable = GetComponent<XRGrabInteractable>();
        //grabbable.activated.AddListener(Shoot);
        //grabbable.selectEntered.AddListener(OnButtonPressed);
        //grabbable.selectExited.AddListener(OnButtonReleased);
    }

    private void Update()
    {
        // Set text
        text.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void OnEnable()
    {
        grabbable.selectEntered.AddListener(OnButtonPressed);
        grabbable.selectExited.AddListener(OnButtonReleased);
    }

    private void OnDisable()
    {
        grabbable.selectEntered.RemoveListener(OnButtonPressed);
        grabbable.selectExited.RemoveListener(OnButtonReleased);
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        if (!allowButtonHold) Shoot();
    }

    private void OnButtonReleased(SelectExitEventArgs args)
    {
        if (allowButtonHold) Shoot();
    }

    private void Shoot()
    {
        if (!readyToShoot || reloading || bulletsLeft <= 0) return;

        bulletsShot = bulletsPerTap;

        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate direction with spread
        Vector3 direction = attackPoint.transform.forward + new Vector3(x, y, 0);

        // Raycast
        RaycastHit rayHit;
        if (Physics.Raycast(attackPoint.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy")) ;
                //rayHit.collider.GetComponent<ShootingAi>().TakeDamage(damage);
        }


        // Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    public void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
