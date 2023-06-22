using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLightToggle : MonoBehaviour
{
    // The spotlight component
    public Light spotlight;

    // The mesh renderer for the light ray
    public MeshRenderer rayMesh;

    // The material to use when the light is on
    public Material onMaterial;

    // The material to use when the light is off
    public Material offMaterial;

    // The mesh renderer for the light bulb
    public MeshRenderer bulbMesh;

    // Set the initial state of the ray mesh and bulb material based on the spotlight state
    private void Start()
    {
        rayMesh.enabled = spotlight.enabled;
        bulbMesh.material = spotlight.enabled ? onMaterial : offMaterial;
    }

    // Update the ray mesh and bulb material based on the spotlight state
    private void Update()
    {
        // Check if the spotlight state has changed since the last frame
        if (spotlight.enabled != rayMesh.enabled)
        {
            // Update the ray mesh state to match the spotlight state
            rayMesh.enabled = spotlight.enabled;

            // Update the material of the bulb to match the spotlight state
            bulbMesh.material = spotlight.enabled ? onMaterial : offMaterial;
        }
    }
}
