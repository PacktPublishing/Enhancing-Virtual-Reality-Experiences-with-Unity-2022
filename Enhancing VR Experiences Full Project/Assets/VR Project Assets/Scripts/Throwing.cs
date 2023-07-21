using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

 

public class Throwing : MonoBehaviour
{
[Header("References")]
public Transform cam;
public Transform attackPoint; //add to camera GO
public GameObject objectToThrow;

[Header("Settings")]
public int totalThrows;
public float throwCooldown;

[Header("Throwing")]
//input action throwkey
public float throwForce;
public float throwUpwardForce;
public float throwRange;
bool readyToThrow;

private void Start() {
    readyToThrow = true;
}

private void Update() {
    //press input key
    if(readyToThrow&&totalThrows>0)//inputaction pressed
    {
Throw();
    }
}
public void Throw(){
    readyToThrow = false;
    GameObject projectile = Instantiate(objectToThrow,attackPoint.position,cam.rotation);
    Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
    Vector3 forceDirection = cam.transform.forward;
    RaycastHit hit;
    if(Physics.Raycast(cam.position,cam.forward, out hit, throwRange))
    {
forceDirection=(hit.point-attackPoint.position).normalized;
    }
    Vector3 forceToAdd = forceDirection*throwForce+transform.up*throwUpwardForce;
    projectileRb.AddForce(forceToAdd,ForceMode.Impulse);
    totalThrows--;
    Invoke(nameof(ResetThrow),throwCooldown);
}

public void ResetThrow(){
    readyToThrow = true;
}
}