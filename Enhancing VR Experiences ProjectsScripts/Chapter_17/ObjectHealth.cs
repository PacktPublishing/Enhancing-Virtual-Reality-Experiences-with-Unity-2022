using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    // The initial health value
    public float health = 5f;
    // The prefab of the shattered object
    public GameObject shatterPrefab;
    // The time delay before destroying the shattered object
    public float shatterTime = 2f;

    public void TakeDamage(float amount)
    {
        // Reduce the health by the specified amount
        health -= amount;

        // Check if the health is depleted
        if (health <= 0f)
        {
            // Shatter the object
            Shatter();
        }
    }

    void Shatter()
    {
        // Instantiate the shatter effect at the object's position and rotation
        GameObject shatterEffect = Instantiate(shatterPrefab, transform.position, transform.rotation);

        // Destroy the shatter effect after the specified time delay
        Destroy(shatterEffect, shatterTime);

        // Destroy the object and its children
        DestroyGameObjectAndChildren(gameObject);
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
