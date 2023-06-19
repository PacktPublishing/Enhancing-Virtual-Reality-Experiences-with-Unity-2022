using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleScale : MonoBehaviour
{
    // The input action asset containing the defined input actions
    public InputActionAsset inputActionAsset;

    // Input action reference for scaling up
    public InputActionReference scaleUp;

    // Input action reference for scaling down
    public InputActionReference scaleDown;

    private Vector3 initialScale;
    private bool isScalingUp;
    private bool isScalingDown;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial scale of the object
        initialScale = transform.localScale;

        // Enable the input action asset
        inputActionAsset.Enable();

        // Attach event handlers for scaling actions
        scaleUp.action.performed += ctx => isScalingUp = true;
        scaleUp.action.canceled += ctx => isScalingUp = false;
        scaleDown.action.performed += ctx => isScalingDown = true;
        scaleDown.action.canceled += ctx => isScalingDown = false;
    }

    private void OnDestroy()
    {
        // Disable the input action asset when the script is destroyed
        inputActionAsset.Disable();
    }

    // Update is called once per frame
    private void Update()
    {
        // Scale the object based on the scaling flags
        if (isScalingUp)
        {
            transform.localScale += new Vector3(.01f, .01f, .01f);
        }
        else if (isScalingDown)
        {
            transform.localScale -= new Vector3(.01f, .01f, .01f);
        }
    }

    // Scale the object up
    public void ScaleUp()
    {
        isScalingUp = true;
        isScalingDown = false;
    }

    // Scale the object down
    public void ScaleDown()
    {
        isScalingUp = false;
        isScalingDown = true;
    }

    // Stop scaling the object
    public void ScaleNull()
    {
        isScalingUp = false;
        isScalingDown = false;
    }
}
