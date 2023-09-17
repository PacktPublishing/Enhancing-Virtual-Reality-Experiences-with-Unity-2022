using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ColliderInputController : MonoBehaviour
{
    public InputActionAsset actionAsset;
    public InputActionReference actionReference;
    public string colliderTag;
    public UnityEvent enterEvent;
    public UnityEvent stayEvent;
    public UnityEvent exitEvent;
    public UnityEvent actionEvent;


    private void OnEnable()
    {
        actionAsset.Enable();
    }
    private void OnDisable()
    {
        actionAsset.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == colliderTag)
        {
            enterEvent.Invoke();
            actionReference.action.performed += ctx => PerformedAction(ctx);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == colliderTag)
        {
            stayEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == colliderTag)
        {
            actionReference.action.performed -= ctx => PerformedAction(ctx);
            exitEvent.Invoke();
        }
    }

    private void PerformedAction(InputAction.CallbackContext ctx)
    {
        actionEvent.Invoke();
    }
}
