using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRRigMenuSettings : MonoBehaviour
{
    // UI sliders for adjusting the settings
    public Slider jumpForce;
    public Slider speedBoost;
    public Slider crouchSpeed;

    // References to the corresponding action scripts
    private JumpingAction jumpingAction;
    private SpeedAction speedAction;
    private CrouchingAction crouchingAction;

    // Start is called before the first frame update
    void Start()
    {
        // Get references to the action scripts
        jumpingAction = GetComponent<JumpingAction>();
        speedAction = GetComponent<SpeedAction>();
        crouchingAction = GetComponent<CrouchingAction>();

        // Set the initial values of the sliders based on the action script values
        jumpForce.value = jumpingAction.jumpForce;
        speedBoost.value = speedAction.speedBoost;
        crouchSpeed.value = crouchingAction.crouchSpeedModifier;

        // Add listeners to the sliders for value change events
        jumpForce.onValueChanged.AddListener(OnJumpForceChanged);
        speedBoost.onValueChanged.AddListener(OnSpeedBoostChanged);
        crouchSpeed.onValueChanged.AddListener(OnCrouchSpeedChanged);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the action script values based on the slider values
        jumpingAction.jumpForce = jumpForce.value;
        speedAction.speedBoost = speedBoost.value;
        crouchingAction.crouchSpeedModifier = crouchSpeed.value;
    }

    // Event handler for jump force value change
    void OnJumpForceChanged(float value)
    {
        // Update the jump force value in the JumpingAction script
        jumpingAction.jumpForce = value;
    }

    // Event handler for speed boost value change
    void OnSpeedBoostChanged(float value)
    {
        // Update the speed boost value in the SpeedAction script
        speedAction.speedBoost = value;
    }

    // Event handler for crouch speed value change
    void OnCrouchSpeedChanged(float value)
    {
        // Update the crouch speed value in the CrouchingAction script
        crouchingAction.crouchSpeedModifier = value;
    }
}
