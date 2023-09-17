using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class ToggleInputActionManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPressedActions
    {
        public string actionName;
        public InputActionReference actionReference;

        public UnityEvent actionEvent = new UnityEvent();
        public bool showPressedAndReleasedEvents; // new boolean field
        public UnityEvent pressedEvent = new UnityEvent();
        public UnityEvent releasedEvent = new UnityEvent();
    }

    public InputActionAsset actionAsset;
    public ButtonPressedActions[] actions;


    private void Awake()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null)
            {
                actions[i].actionReference.action.performed += OnButtonPerformed;
                actions[i].actionReference.action.started += OnButtonPressed;
                actions[i].actionReference.action.canceled += OnButtonReleased;
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].actionReference.action.started -= OnButtonPressed;
            actions[i].actionReference.action.canceled -= OnButtonReleased;
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].actionReference.action.Enable();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].actionReference.action.Disable();
        }
    }

    private void OnButtonPerformed(InputAction.CallbackContext context)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null && actions[i].actionReference.action == context.action)
            {
                actions[i].actionEvent.Invoke();
            }
        }
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null && actions[i].actionReference.action == context.action)
            {
                if (actions[i].showPressedAndReleasedEvents)
                {
                    actions[i].pressedEvent.Invoke();
                }
            }
        }
    }

    private void OnButtonReleased(InputAction.CallbackContext context)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null && actions[i].actionReference.action == context.action)
            {
                if (actions[i].showPressedAndReleasedEvents)
                {
                    actions[i].releasedEvent.Invoke();
                }
            }
        }
    }
}