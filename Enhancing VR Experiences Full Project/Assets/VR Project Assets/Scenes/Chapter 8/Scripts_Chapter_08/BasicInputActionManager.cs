using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class BasicInputActionManager : MonoBehaviour
{
    public InputActionAsset actionAsset;
    public InputActionReference actionReference;
    public UnityEvent actionEvent;


    private void Awake()
    {
        actionReference.action.performed += OnButtonPressed;
    }
    private void OnDestroy()
    {
        actionReference.action.performed -= OnButtonPressed;
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        actionEvent.Invoke();
    }
}
