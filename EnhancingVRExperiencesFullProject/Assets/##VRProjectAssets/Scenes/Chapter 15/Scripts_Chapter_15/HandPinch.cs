using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandPinch : MonoBehaviour
{
    public InputActionProperty pinch;
    public InputActionProperty grip;
    public Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinch.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);
        float gripValue = grip.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
