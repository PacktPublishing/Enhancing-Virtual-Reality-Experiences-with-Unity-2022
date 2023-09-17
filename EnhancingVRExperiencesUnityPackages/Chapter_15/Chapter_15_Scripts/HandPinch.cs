using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandPinch : MonoBehaviour
{
    // The InputActionProperty for the pinch action
    public InputActionProperty pinch;

    // The InputActionProperty for the grip action
    public InputActionProperty grip;

    // The Animator component for the hand animation
    public Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code here
    }

    // Update is called once per frame
    void Update()
    {
        // Read the value of the pinch action
        float triggerValue = pinch.action.ReadValue<float>();

        // Set the "Trigger" parameter in the hand animator to the pinch value
        handAnimator.SetFloat("Trigger", triggerValue);

        // Read the value of the grip action
        float gripValue = grip.action.ReadValue<float>();

        // Set the "Grip" parameter in the hand animator to the grip value
        handAnimator.SetFloat("Grip", gripValue);
    }
}
