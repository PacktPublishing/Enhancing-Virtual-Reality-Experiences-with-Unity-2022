using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

// Manages input actions and invokes events based on the performed, pressed, and released states of the input actions.
public class ToggleInputActionManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonPressedActions
    {
        // The name of the input action
        public string actionName;

        // The reference to the input action
        public InputActionReference actionReference;

        // The event to invoke when the input action is performed
        public UnityEvent actionEvent = new UnityEvent();

        // Indicates whether to show pressed and released events
        public bool showPressedAndReleasedEvents;

        // The event to invoke when the input action is pressed
        public UnityEvent pressedEvent = new UnityEvent();

        // The event to invoke when the input action is released
        public UnityEvent releasedEvent = new UnityEvent();
    }

    // The input action asset
    public InputActionAsset actionAsset;

    // The array of button pressed actions
    public ButtonPressedActions[] actions;

    // Subscribe to the input action events.
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

    // Unsubscribe from the input action events.
    private void OnDestroy()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null)
            {
                actions[i].actionReference.action.started -= OnButtonPressed;
                actions[i].actionReference.action.canceled -= OnButtonReleased;
            }
        }
    }

    // Enable the input actions.
    private void OnEnable()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null)
            {
                actions[i].actionReference.action.Enable();
            }
        }
    }

    // Disable the input actions.
    private void OnDisable()
    {
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionReference != null)
            {
                actions[i].actionReference.action.Disable();
            }
        }
    }

    // Invoke the action event for the performed input action.
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

    // Invoke the pressed event for the pressed input action.
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

    // Invoke the released event for the released input action.
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
