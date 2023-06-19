using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    // The tag assigned to the player GameObject
    public string playerTag;

    // The name of the destination portal
    public string destinationName;

    // The GameObject representing the destination portal
    public GameObject destination;

    // The GameObject representing the player
    public GameObject player;

    // The cooldown time for the portal
    public float portalCooldown;

    private float cooldownTime;
    private bool onCooldown;

    // The transform of the portal anchor
    public Transform portalAnchor;

    private void Start()
    {
        cooldownTime = 0.0f;
        onCooldown = false;
        player = GameObject.FindWithTag("Player");
        portalAnchor = GetComponentInChildren<Transform>().Find("PortalAnchor1");

        // Find the player GameObject using the player tag
        player = GameObject.FindWithTag(playerTag);

        // Find the transform of the child object named "PortalAnchor1"
        portalAnchor = GetComponentInChildren<Transform>().Find("PortalAnchor1");
    }

    private void Update()
    {
        if (onCooldown)
        {
            // Decrease the cooldown time based on Time.deltaTime
            cooldownTime -= Time.deltaTime;

            // Check if the cooldown time has reached or gone below zero
            if (cooldownTime <= 0.0f)
            {
                // Reset the cooldown flag
                onCooldown = false;
            }
        }
    }

    // Start the cooldown for the portal
    public void StartCoolDown()
    {
        // Check if the portal is not on cooldown
        if (!onCooldown)
        {
            // Set the cooldown time and enable the cooldown flag
            cooldownTime = portalCooldown;
            onCooldown = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider's GameObject tag matches the player tag and the portal is not on cooldown
        if (other.gameObject.tag == playerTag && !onCooldown)
        {
            // Start the cooldown
            StartCoolDown();

            // Teleport the player to the destination portal's position
            player.transform.position = destination.transform.position;
        }
    }
}
