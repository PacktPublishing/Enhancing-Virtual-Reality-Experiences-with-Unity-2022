using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

// Requires a Rigidbody component for physics interactions
[RequireComponent(typeof(Rigidbody))]
public class JumpingAction : MonoBehaviour
{
    // The force applied when jumping
    public float jumpForce;

    // The radius for ground detection
    public float groundCheckRadius = 0.5f;

    // The layer(s) considered as ground
    public LayerMask groundLayer;

    // The point used for ground detection
    public Transform groundCheckPoint;

    private Rigidbody _rigidbody; // Reference to the Rigidbody component

    // Flag indicating if the jump is being charged
    public bool jumpCharging;

    // Flag indicating if a jump is in progress
    public bool jumping;

    // Timer for tracking jump charge time
    public float chargeTimer;

    // Finalized jump charge time
    public float finalTimer;

    // Maximum allowed jump charge time
    public float maxChargeTime = 1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (jumpCharging)
        {
            finalTimer = chargeTimer += Time.deltaTime;
        }
    }

    // Checks if the character is on the ground
    private bool IsGrounded()
    {
        return Physics.OverlapSphere(groundCheckPoint.position, groundCheckRadius, groundLayer).Length > 0;
    }

    // Called when the jump button is pressed
    public void JumpPressed()
    {
        if (IsGrounded())
        {
            jumpCharging = true;
        }
    }

    // Called when the jump button is released
    public void JumpRelease()
    {
        jumpCharging = false;
        if (IsGrounded())
        {
            JumpCharge(finalTimer);
        }
    }

    // Applies a jump force based on the charge level
    public void JumpCharge(float jumpForceMultiplier)
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce * jumpForceMultiplier, ForceMode.Impulse);
            Debug.Log("Jumped!");
            chargeTimer = 0f;
            finalTimer = 0f;
        }
    }

    // Performs a regular jump without charging
    public void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jumped!");
        }
    }

    // Draws a wire sphere to visualize the ground check point
    private void OnDrawGizmos()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
