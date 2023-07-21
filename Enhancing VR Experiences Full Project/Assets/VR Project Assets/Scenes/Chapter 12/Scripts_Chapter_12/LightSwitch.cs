using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public bool lightsOn = true;                // Whether the lights are currently on or off
    //public List<Light> lightsToSwitch;          // The list of lights to turn on or off
    public Renderer buttonMesh;                 // The mesh renderer for the button
    public Material onMaterial;                 // The material to use when the lights are on
    public Material offMaterial;                // The material to use when the lights are off
    public float toggleRadius = 5.0f;           // The radius within which to toggle the lights
    public LayerMask toggleLayer;               // The layer on which to toggle the lights

    void Start()
    {
        // Set the initial state of the lights and button material based on the "lightsOn" variable
        SetLights(lightsOn);
        SetButtonMaterial(lightsOn);
    }

    // Toggle the lights on or off and change the button material accordingly
    public void ToggleLights()
    {
        lightsOn = !lightsOn;
        SetLights(lightsOn);
        SetButtonMaterial(lightsOn);
    }

    // Set the lights on or off based on the given state
    void SetLights(bool state)
    {
        // Find all colliders within the toggle radius on the toggle layer
        Collider[] colliders = Physics.OverlapSphere(transform.position, toggleRadius, toggleLayer);

        // Loop through the colliders and toggle the lights if they have a "Light" component
        foreach (Collider collider in colliders)
        {
            Light light = collider.GetComponent<Light>();
            if (light != null)
            {
                light.enabled = state;
            }
        }
        // Loop through the list of lights and set their "enabled" property to the given state
        //foreach (Light light in lightsToSwitch)
        //{
        //    light.enabled = state;
        //}
    }

    // Set the button material based on the given state
    void SetButtonMaterial(bool state)
    {
        if (state)
        {
            buttonMesh.material = onMaterial;
        }
        else
        {
            buttonMesh.material = offMaterial;
        }
    }
    // Draw a wire sphere to show the toggle radius in the Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, toggleRadius);
    }
}
