using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Toggle between the direct and ray interactor if the direct interactor isn't touching any objects
/// Should be placed on a ray interactor
/// </summary>
[RequireComponent(typeof(XRRayInteractor))]
public class ToggleRay : MonoBehaviour
{
    // Switch even if an object is selected
    [Tooltip("Switch even if an object is selected")]
    public bool forceToggle = false;

    // The direct interactor that's switched to
    [Tooltip("The direct interactor that's switched to")]
    public XRDirectInteractor directInteractor = null;

    private XRRayInteractor rayInteractor = null;
    private bool isSwitched = false;

    private void Awake()
    {
        // Get the XRRayInteractor component attached to this GameObject
        rayInteractor = GetComponent<XRRayInteractor>();

        // Initially, switch to the ray interactor
        SwitchInteractors(false);
    }

    public void ActivateRay()
    {
        // Check if the direct interactor isn't touching any objects or forceToggle is enabled
        if (!TouchingObject() || forceToggle)
            SwitchInteractors(true);
    }

    public void DeactivateRay()
    {
        // Check if the interactor has been switched to the direct interactor
        if (isSwitched)
            SwitchInteractors(false);
    }

    private bool TouchingObject()
    {
        // Create a list to store valid targets
        List<XRBaseInteractable> targets = new List<XRBaseInteractable>();

        // Get the valid targets using the direct interactor
        directInteractor.GetValidTargets(targets);

        // Return true if there are any valid targets, indicating that the direct interactor is touching an object
        return (targets.Count > 0);
    }

    private void SwitchInteractors(bool value)
    {
        // Update the switched state
        isSwitched = value;

        // Enable or disable the ray interactor based on the switched state
        rayInteractor.enabled = value;

        // Enable or disable the direct interactor based on the switched state
        directInteractor.enabled = !value;
    }
}
