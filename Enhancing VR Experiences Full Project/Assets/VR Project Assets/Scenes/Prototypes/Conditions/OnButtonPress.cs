using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Checks for button input on an input action
/// </summary>
public class OnButtonPress : MonoBehaviour
{
    [Tooltip("Actions to check")]
    public InputAction action = null;
    public InputActionProperty actionButton;
    // When the button is pressed
    public UnityEvent OnPress = new UnityEvent();

    // When the button is released
    public UnityEvent OnRelease = new UnityEvent();

    private void Awake()
    {
        actionButton. action.started += Pressed;
        actionButton.action.canceled += Released;
    }

    private void OnDestroy()
    {
        actionButton.action.started -= Pressed;
        actionButton.action.canceled -= Released;
    }

    private void OnEnable()
    {
        actionButton.action.Enable();
    }

    private void OnDisable()
    {
        actionButton.action.Disable();
    }

    private void Pressed(InputAction.CallbackContext context)
    {
        OnPress.Invoke();
    }

    private void Released(InputAction.CallbackContext context)
    {
        OnRelease.Invoke();
    }
}
