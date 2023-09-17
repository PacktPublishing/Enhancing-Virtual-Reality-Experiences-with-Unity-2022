using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorChanger : MonoBehaviour
{
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get all Renderer components in the hierarchy of this object
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        // Iterate over each Renderer component
        foreach (Renderer renderer in renderers)
        {
            // Get the materials of the current Renderer
            Material[] materials = renderer.materials;

            // Iterate over each material
            for (int i = 0; i < materials.Length; i++)
            {
                // Generate a random color and assign it to the material
                materials[i].color = new Color(Random.value, Random.value, Random.value, 1f);
            }

            // Assign the modified materials back to the Renderer
            renderer.materials = materials;
        }
    }
}
