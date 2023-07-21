using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CielingLightToggle : MonoBehaviour
{
    public Light spotlight;
    public MeshRenderer rayMesh;
    public Material onMaterial;
    public Material offMaterial;
    public MeshRenderer bulbMesh;

    private void Start()
    {
        // Set the initial state of the ray mesh based on the spotlight state
        rayMesh.enabled = spotlight.enabled;
        // Set the initial material of the bulb based on the spotlight state
       
        bulbMesh.material = spotlight.enabled ? onMaterial : offMaterial;
    }

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
