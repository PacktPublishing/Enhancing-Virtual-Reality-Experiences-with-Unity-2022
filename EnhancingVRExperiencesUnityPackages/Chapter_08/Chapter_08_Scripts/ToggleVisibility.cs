using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    // Toggle the visibility of the game object
    public void ToggleVisibility()
    {
        // Invert the current active state of the game object
        bool isActive = !gameObject.activeSelf;

        // Set the game object's active state to the inverted value
        gameObject.SetActive(isActive);
    }
}
