using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRigMenuManager : MonoBehaviour
{
    // The menu game object
    public GameObject menu;

    // The head transform
    public Transform head;

    // The distance from the head to spawn the menu
    public float spawnDistance = 2;

    // Toggles the visibility of the menu
    public void ToggleMenu()
    {
        // Activate or deactivate the menu game object
        menu.SetActive(!menu.activeSelf);

        // Position the menu in front of the head at a specified distance
        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;

        // Make the menu face the head
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));

        // Reverse the forward direction of the menu
        menu.transform.forward *= -1;
    }
}
