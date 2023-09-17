using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputActionManager : MonoBehaviour
{
    [System.Serializable]
    public class CustomActions
    {
        // The name of the action
        public string actionName;

        // Reference to the input action within the action asset
        public InputActionReference actionReference;

        // UnityEvent to be invoked when the input action is triggered
        public UnityEvent actionEvent;
    }

    // The input action asset containing the defined actions
    public InputActionAsset actionAsset;

    // Array of custom actions
    public CustomActions[] actions;

    private void Awake()
    {
        // Attach the OnButtonPressed method to the performed event of each action
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null)
            {
                actions[i].actionReference.action.performed += OnButtonPressed;
            }
        }
    }

    private void OnDestroy()
    {
        // Remove the OnButtonPressed method from the performed event of each action
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null)
            {
                actions[i].actionReference.action.performed -= OnButtonPressed;
            }
        }
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        // Invoke the corresponding actionEvent when an input action is triggered
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null && actions[i].actionReference.action == context.action)
            {
                actions[i].actionEvent.Invoke();
            }
        }
    }
}
