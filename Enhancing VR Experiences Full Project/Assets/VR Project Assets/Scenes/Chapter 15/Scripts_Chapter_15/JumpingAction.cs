using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class JumpingAction : MonoBehaviour
{
    //[SerializeField] private InputActionReference jumpAction;
    public float jumpForce;
    [SerializeField] private float groundCheckRadius = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPoint;
    private Rigidbody _rigidbody;

    public bool jumpCharging;
    public bool jumping;
    public float chargeTimer;
    public float finalTimer;
    public float maxChargeTime = 1f;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //jumpAction.action.performed += OnJumpAction;
    }
    private void Update()
    {
        if (jumpCharging)
        {
            finalTimer = chargeTimer += Time.deltaTime;
        }

    }
    private void OnDestroy()
    {
        //jumpAction.action.performed -= OnJumpAction;
    }
    private bool IsGrounded()
    {
        return Physics.OverlapSphere(groundCheckPoint.position, groundCheckRadius, groundLayer).Length > 0;
    }


    public void JumpPressed()
    {
        if (IsGrounded())
        {
            jumpCharging = true;
        }
    }
    public void JumpRelease()
    {
        jumpCharging = false;
        if (IsGrounded())
        {
            // The button has been released, jump with the current charge
            JumpCharge(finalTimer);
            
            

        }
    }

    public void JumpCharge(float jumpForceMultiplier)
    {
        if (IsGrounded())
        {

            //float jumpForceMultiplier = Mathf.Clamp01(chargePercent);
            _rigidbody.AddForce(Vector3.up * jumpForce * jumpForceMultiplier, ForceMode.Impulse);
            Debug.Log(jumpForceMultiplier);
            Debug.Log("Jumped!");
            chargeTimer = 0f;
            finalTimer = 0f;
        }
    }

    public void Jump()
    {
        if (IsGrounded())
        {
           
            //float jumpForceMultiplier = Mathf.Clamp01(chargePercent);
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jumped!");

        }
    }
    private void OnDrawGizmos()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
