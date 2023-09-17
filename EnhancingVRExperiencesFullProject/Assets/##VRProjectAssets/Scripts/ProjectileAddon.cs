using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileAddon : MonoBehaviour
{

    public int damage;
private Rigidbody rb;//set to projectile script
private bool targetHit;
private void Start() {
    rb = GetComponent<Rigidbody>();

}

    private void OnCollisionEnter(Collision other) 
    {
        if (targetHit)
        
            return;
        
        
        else
            targetHit = true;
        if (other.gameObject.GetComponent<BasicEnemy>()!=null)
        {
                BasicEnemy enemy =  other.gameObject.GetComponent<BasicEnemy>();
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
            rb.isKinematic = true;
            transform.SetParent(this.transform);
    }

}