using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    public float health = 5f;
    public GameObject shatterPrefab;
    public float shatterTime = 2f;
    public void TakeDamage(float amount) 
    {
        health -= amount;
        if (health <= 0f)
        {
            Shatter();
        }
    }

    void Shatter()
    {
        Instantiate(shatterPrefab, transform.position, transform.rotation);
        Destroy(shatterPrefab, shatterTime);
        DestroyGameObjectAndChildren(this.gameObject);

    }

    void DestroyGameObjectAndChildren(GameObject gameObject)
    {
        // Destroy all children first
        foreach (Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }

        // Then destroy the parent gameobject
        Destroy(gameObject);
    }


}
