using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class BasicInputActionManager : MonoBehaviour
{
    // The InputActionAsset containing the input actions
    public InputActionAsset actionAsset;

    // A reference to the specific input action within the asset
    public InputActionReference actionReference;

    // An event that will be invoked when the input action is triggered
    public UnityEvent actionEvent;

    private void Awake()
    {
        // Attach a callback method to the performed event of the input action
        actionReference.action.performed += OnButtonPressed;
    }

    private void OnDestroy()
    {
        // Remove the callback method from the performed event to avoid memory leaks
        actionReference.action.performed -= OnButtonPressed;
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        // Invoke the actionEvent UnityEvent when the input action is triggered
        actionEvent.Invoke();
    }
}
