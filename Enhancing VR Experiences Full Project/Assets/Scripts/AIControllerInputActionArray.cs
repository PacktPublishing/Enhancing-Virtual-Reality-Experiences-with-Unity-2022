using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;

public class AIControllerInputActionArray : MonoBehaviour
{
    [System.Serializable]
    public class ActionsButtons
    {
        public string eventName;
        public InputActionReference buttonReference;
        public UnityEvent buttonEvent;
    }
    public InputActionAsset inputActionAsset;
    public ActionsButtons[] actions;
    private void Awake()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            // Make sure the buttonReference is not null before adding the event
            if (actions[i].buttonReference != null)
            {
                actions[i].buttonReference.action.performed += OnButtonPressed;
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            // Make sure the buttonReference is not null before removing the event
            if (actions[i].buttonReference != null)
            {
                actions[i].buttonReference.action.performed -= OnButtonPressed;
            }
        }
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            // Make sure the buttonReference is not null before invoking the event
            if (actions[i].buttonReference != null && actions[i].buttonReference.action == context.action)
            {
                actions[i].buttonEvent.Invoke();
            }
        }
    }
}

