using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ColliderInputController : MonoBehaviour
{
    // The input action asset containing the defined input actions
    public InputActionAsset actionAsset;

    // The input action reference for the desired action
    public InputActionReference actionReference;

    // The tag of the colliders that will trigger the events
    public string colliderTag;

    // UnityEvent invoked when an object enters the collider
    public UnityEvent enterEvent;

    // UnityEvent invoked while an object stays within the collider
    public UnityEvent stayEvent;

    // UnityEvent invoked when an object exits the collider
    public UnityEvent exitEvent;

    // UnityEvent invoked when the input action is performed
    public UnityEvent actionEvent;

    private void OnEnable()
    {
        // Enable the input action asset
        actionAsset.Enable();
    }

    private void OnDisable()
    {
        // Disable the input action asset
        actionAsset.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == colliderTag)
        {
            // Invoke the enterEvent when the collider is entered by an object with the matching tag
            enterEvent.Invoke();

            // Attach the PerformedAction method to the performed event of the input action
            actionReference.action.performed += ctx => PerformedAction(ctx);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == colliderTag)
        {
            // Invoke the stayEvent while an object with the matching tag stays within the collider
            stayEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == colliderTag)
        {
            // Detach the PerformedAction method from the performed event of the input action
            actionReference.action.performed -= ctx => PerformedAction(ctx);

            // Invoke the exitEvent when an object with the matching tag exits the collider
            exitEvent.Invoke();
        }
    }

    private void PerformedAction(InputAction.CallbackContext ctx)
    {
        // Invoke the actionEvent when the input action is performed
        actionEvent.Invoke();
    }
}
