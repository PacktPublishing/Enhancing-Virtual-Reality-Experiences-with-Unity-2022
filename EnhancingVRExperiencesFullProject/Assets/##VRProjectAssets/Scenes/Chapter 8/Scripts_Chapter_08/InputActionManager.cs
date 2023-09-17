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
        public string actionName;
        public InputActionReference actionReference;
        public UnityEvent actionEvent;
    }

    public InputActionAsset actionAsset;
    public CustomActions[] actions;

    private void Awake()
    {
        for(int i=0; i < actions.Length; i++)
        {
            if(actions[i].actionReference != null)
            {
                actions[i].actionReference.action.performed += OnButtonPressed;

            }
        }
        
    }

    private void OnDestroy()
    {
        for (int i = 0; i < actions.Length; i++)
        {
           
                actions[i].actionReference.action.performed -= OnButtonPressed;

            
        }
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null && actions[i].actionReference.action==context.action)
            actions[i].actionEvent.Invoke();


        }
    }
}
