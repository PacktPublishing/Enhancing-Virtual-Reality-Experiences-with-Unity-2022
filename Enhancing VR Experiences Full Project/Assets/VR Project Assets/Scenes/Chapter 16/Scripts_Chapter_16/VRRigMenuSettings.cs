using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRRigMenuSettings : MonoBehaviour
{
    public Slider jumpForce;
    public Slider speedBoost;
    public Slider crouchSpeed;
    private JumpingAction jumpingAction;
    private SpeedAction speedAction;
    private CrouchingAction crouchingAction;

    // Start is called before the first frame update
    void Start()
    {
        jumpingAction = GetComponent<JumpingAction>();
        jumpForce.value = jumpingAction.jumpForce;
        speedAction = GetComponent<SpeedAction>();
       // speedBoost.value = speedAction.speedBoost;
        crouchingAction = GetComponent<CrouchingAction>();
        crouchSpeed.value = crouchingAction.crouchSpeedModifier;

        jumpForce.onValueChanged.AddListener(OnJumpForceChanged);
        speedBoost.onValueChanged.AddListener(OnSpeedBoostChanged);
        crouchSpeed.onValueChanged.AddListener(OnCrouchSpeedChanged);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //    jumpingAction.jumpForce = jumpForce.value;
    //    speedAction.speedBoost = speedBoost.value;
    //    crouchingAction.crouchSpeedModifier = crouchSpeed.value;
    //}


    void OnJumpForceChanged(float value)
    {
        // Update the jump force value in the JumpingAction script
        jumpingAction.jumpForce = value;
    }

    void OnSpeedBoostChanged(float value)
    {
        // Update the speed boost value in the SpeedAction script
        speedAction.speedBoost = value;
    }

    void OnCrouchSpeedChanged(float value)
    {
        // Update the crouch speed value in the CrouchingAction script
        crouchingAction.crouchSpeedModifier = value;
    }


}
