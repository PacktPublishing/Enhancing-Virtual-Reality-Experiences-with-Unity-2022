using UnityEngine;
using UnityEngine.InputSystem;

public class ScaleController : MonoBehaviour
{
    // Declare a public InputActionAsset variable to reference the input action asset
    public InputActionAsset inputActionAsset;
    // Declare public InputActionReference variables to reference the scale up and scale down actions 
    public InputActionReference scaleUpActionReference;
    public InputActionReference scaleDownActionReference;

    // Declare a Vector3 variable to store the initial scale of the game object
    private Vector3 initialScale;
    // Declare boolean variables to track if the scale up or scale down actions are being performed
    private bool isScalingUp;
    private bool isScalingDown;

    private void Start()
    {
        // Store the initial scale of the game object
        initialScale = transform.localScale;
        // Enable the input action asset to make the actions available
        inputActionAsset.Enable();
        // Add a listener for the performed event of the scale up action
        scaleUpActionReference.action.performed += ctx => isScalingUp = true;
        // Add a listener for the canceled event of the scale up action
        scaleUpActionReference.action.canceled += ctx => isScalingUp = false;
        // Add a listener for the performed event of the scale down action
        scaleDownActionReference.action.performed += ctx => isScalingDown = true;
        // Add a listener for the canceled event of the scale down action
        scaleDownActionReference.action.canceled += ctx => isScalingDown = false;
    }
    // Disable the input action asset when the script is disabled
    private void OnDisable()
    {
        inputActionAsset.Disable();
    }

    private void Update()
    {
        // Check if the scale up action is being performed
        if (isScalingUp)
        {
            // Increase the scale of the game object
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
        }
        // Check if the scale down action is being performed
        else if (isScalingDown)
        {
            // Decrease the scale of the game object
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}