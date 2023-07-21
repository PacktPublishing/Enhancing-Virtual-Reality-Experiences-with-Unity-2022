using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(SpeedAction))]
[RequireComponent(typeof(Rigidbody))]
public class CrouchingAction : MonoBehaviour
{
    [Header("Crouching")]
    // The speed modifier applied when crouching
    private float crouchSpeed;

    // The speed boost modifier applied when crouching
    private float crouchSpeedBoost;

    // The modifier for crouch speed and speed boost
    [Range(0f, 1f)]
    public float crouchSpeedModifier;

    // The scale value for the Y-axis when crouching
    public float crouchYScale;

    // The starting scale value for the Y-axis
    public float startYScale;

    // Flag indicating if the character is currently crouching
    public bool isCrouching;

    [Header("References")]
    // The orientation transform used for movement
    public Transform orientation;

    // Reference to the Rigidbody component
    public Rigidbody rb;

    // Reference to the ContinuousMoveProviderBase component
    public ContinuousMoveProviderBase moveProvider;

    // Reference to the SpeedAction component
    public SpeedAction speedAction;

    private float currentSpeed;
    private float currentSpeedBoost;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveProvider = FindObjectOfType<ContinuousMoveProviderBase>();
        speedAction = FindObjectOfType<SpeedAction>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = moveProvider.moveSpeed;
        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate crouch speed and speed boost based on the modifier
        crouchSpeed = moveProvider.moveSpeed * crouchSpeedModifier;
        crouchSpeedBoost = speedAction.speedBoost * crouchSpeedModifier;

        if (isCrouching)
        {
            // Perform crouching actions
            Crouched();
        }
        else
        {
            // Perform standing actions
            Standing();
        }
    }

    // Called when crouch button is pressed
    public void CrouchDown()
    {
        isCrouching = true;
    }

    // Called when crouch button is released
    public void StandUp()
    {
        isCrouching = false;
    }

    // Actions to perform when crouching
    private void Crouched()
    {
        // Set the character's scale to crouched scale
        transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);

        // Apply downward force to simulate crouching
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        // Adjust the move speed and speed boost to crouch values
        moveProvider.moveSpeed = crouchSpeed;
        speedAction.speedBoost = crouchSpeedBoost;
    }

    // Actions to perform when standing
    private void Standing()
    {
        // Reset the character's scale to the starting scale
        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);

        // Reset the move speed and speed boost to default values
        moveProvider.moveSpeed = currentSpeed;
        speedAction.speedBoost = currentSpeedBoost;
    }
}
