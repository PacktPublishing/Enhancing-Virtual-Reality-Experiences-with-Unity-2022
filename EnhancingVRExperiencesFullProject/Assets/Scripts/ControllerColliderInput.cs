using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerColliderInput : MonoBehaviour
{
    public InputActionAsset inputActionAsset;
    public InputActionReference actionReference;
    public Collider collider;

    private void OnEnable()
    {
        inputActionAsset.Enable();
    }

    private void OnDisable()
    {
        inputActionAsset.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == collider)
        {
            actionReference.action.performed += ctx => PerformAction(ctx);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == collider)
        {
            actionReference.action.performed -= PerformAction;
        }
    }

    private void PerformAction(InputAction.CallbackContext ctx)
    {
        // Perform the desired action here
        Debug.Log("Controller entered collider and action was performed");
    }
}
