using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(SpeedAction))]
[RequireComponent(typeof(Rigidbody))]
public class CrouchingAction : MonoBehaviour
{
    [Header("Crouching")]
    private float crouchSpeed;
    private float crouchSpeedBoost;
    [Range(0f, 1f)]
    public float crouchSpeedModifier;
    public float crouchYScale;
    public float startYScale;
    public bool isCrouching;
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public ContinuousMoveProviderBase moveProvider;
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
        //crouchSpeed = moveProvider.moveSpeed * crouchSpeedModifier;
       // crouchSpeedBoost = speedAction.speedBoost * crouchSpeedModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //crouchSpeed = moveProvider.moveSpeed * crouchSpeedModifier;
        //crouchSpeedBoost = speedAction.speedBoost * crouchSpeedModifier;
        if (isCrouching)
        {

            Crouched();
        }
        else
        {

            Standing();
        }
    }
    public void CrouchDown()
    {
        isCrouching = true;

    }
    public void StandUp()
    {
        isCrouching = false;

    }
    private void Crouched()
    {

        //if crouch key pressed
        transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        moveProvider.moveSpeed = crouchSpeed;
        speedAction.speedBoost = crouchSpeedBoost;
    }
    private void Standing()
    {
        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        moveProvider.moveSpeed = currentSpeed;
        speedAction.speedBoost = currentSpeedBoost;
    }
}
