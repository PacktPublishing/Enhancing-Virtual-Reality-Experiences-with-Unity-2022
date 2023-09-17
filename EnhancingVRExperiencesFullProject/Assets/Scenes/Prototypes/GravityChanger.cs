using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GravityChanger : MonoBehaviour
{
    public float attractionForce = 50.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Calculate the direction from the player to the attractor
            Vector3 attractionDirection = (transform.position - other.transform.position).normalized;

            // Apply an impulse force to the player in the attraction direction
            Rigidbody playerRigidbody = other.attachedRigidbody;
            playerRigidbody.AddForce(attractionDirection * attractionForce, ForceMode.Impulse);
        }
    }
}
