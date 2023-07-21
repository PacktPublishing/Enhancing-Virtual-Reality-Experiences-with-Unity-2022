using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
[System.Serializable]
public class ActionsButtons
{
    public InputActionAsset inputActionAsset;
    public InputActionReference buttonReference;
    public UnityEvent buttonEvent;
  
}
public class ControllerInputActions : MonoBehaviour
{

    public ActionsButtons[] actions;


    private void Awake() //
    {

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].buttonReference.action.started += OnButtonPressed;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            // Code to be repeated.
            actions[i].buttonReference.action.started -= OnButtonPressed;
        }
    }
        private void OnButtonPressed(InputAction.CallbackContext context)
        { // This method will be called whenever the trigger button is pressed

        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].buttonEvent.Invoke();
    
        }
        }   

}
