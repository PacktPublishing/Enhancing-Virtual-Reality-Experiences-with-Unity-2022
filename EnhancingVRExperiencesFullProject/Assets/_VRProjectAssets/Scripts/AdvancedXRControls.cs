//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

 

//public class AdvancedXRControls : MonoBehaviour
//{
////Setup
////add to VR Rig (player)
////make input actions for left and right velocity
/////Action - Velocity | ActionType - Value | Control Type - Vector3 | Path - controller Deveice Velocity (each hand controller)
////add sphere colider (radius - .25) to main camera 
////Add postprocessing for water

//    public MovementState state;
//public enum MovementState{
//    freeze,
//    walking,
//    sprinting,
//    crouching,
//    swimming

//}
//    public bool freeze;
//    public bool walking;
//    public bool sprinting;
//    public bool crouching;
//    public bool swimmin;
//[Header("References")]
//public Transform orientation;
//public Rigidbody rb;


//[Header("Movement Speeds")]
//public float walkSpeed;
//public float sprintSpeed;
//    public float moveSpeed;

//[Header("Crouching")]
//public float crouchSpeed;
//public float crouchYScale;
//public float startYScale;
//public bool isCrouching;

//[Header("Swimming")]
//public float swimSpeed;
//public bool isSwimming;
//public float swimForce;
//public float dragForce;
//public float minForce;
//public float minTimeBetweenStrokes;
//public InputActionReference leftControllerSwimReference;
//public InputActionReference leftControllerVelocity;
//public InputActionReference rightControllerSwimReference;
//public InputActionReference rightControllerVelocity;
//public Transform trackingReference;
//public Rigidbody swimRB;
//public float swimCoolDownTimer;



////input actions
////--jump key
////--sprint key
////--crouch key
////--climb key
////--flying key

//    [Header("Camera Effects")]
//    public Camera cam;
//    public float grappleFov = 95f;




//private void Awake() {
//    rb = GetComponent<Rigidbody>();
//}
//private void Start() {
//    startYScale = transform.localScale.y;
//}
//private void Update() {
//    // ground check
//        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

   
//    StateHandler();

      
//            rb.drag = 0;
//}
//private void FixedUpdate() 
//{
// if(isSwimming)
//    {
//      SwimmingMovement();
//    }   

//}
//public void StateHandler(){
//    if(grounded)//&& sprint inputaction
//    {
//        state = MovementState.sprinting;
//        moveSpeed = sprintSpeed;
//    }
//    else if (grounded){
//        state = MovementState.walking;
//        moveSpeed = walkSpeed;
//    }
//    else if (isCrouching){
//        state = MovementState.crouching;
//        moveSpeed = crouchSpeed;
//    }
   
//            // Mode - Freeze
//      else  if (freeze)
//        {
//            state = MovementState.freeze;
//            moveSpeed = 0;
//            rb.velocity = Vector3.zero;
//        }



//}

//public void Crouching(){
//    //if crouch key pressed
//    transform.localScale = new Vector3(transform.localScale.x,crouchYScale,transform.localScale.z);
//    rb.AddForce(Vector3.down*5f,ForceMode.Impulse);
//    isCrouching=true;
//}
//public void StandUp(){
//        transform.localScale = new Vector3(transform.localScale.x,startYScale,transform.localScale.z);
//        isCrouching=false;
//}


//public void StartSwimming(){
//isSwimming=true;
//rb.useGravity = false;
//}
//public void SwimmingMovement(){
//      swimCoolDownTimer+=Time.fixedDeltaTime;
//        if(swimCoolDownTimer>minTimeBetweenStrokes&&leftControllerSwimReference.action.IsPressed()&&rightControllerSwimReference.action.IsPressed())
//        {
//            var leftHandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
//            var rightHandVelocity = rightControllerVelocity.action.ReadValue<Vector3>();
//            Vector3 localVelocity = leftHandVelocity+rightHandVelocity;
//            localVelocity*=-1;
//            //swimforce
//            if (localVelocity.sqrMagnitude>minForce*minForce)
//            {
//                Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
//                rb.AddForce(worldVelocity*swimForce,ForceMode.Acceleration);
//                swimCoolDownTimer = 0;
//            }
//            //dragforce
//            if(rb.velocity.sqrMagnitude>.01f)
//            {
//                rb.AddForce(-rb.velocity*dragForce,ForceMode.Acceleration);
//            }
//        }
//}
//public void StopSwimming(){
//    isSwimming=false;
//    rb.useGravity = true;
//}

//    private bool enableMovementOnNextTouch;
//    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
//    {
        

//        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
//        Invoke(nameof(SetVelocity), 0.1f);

//        Invoke(nameof(ResetRestrictions), 3f);
//    }
//        private Vector3 velocityToSet;
//    private void SetVelocity()
//    {
//        enableMovementOnNextTouch = true;
//        rb.velocity = velocityToSet;

//        cam.DoFov(grappleFov);
//    }

//    public void ResetRestrictions()
//    {
       
//        cam.DoFov(85f);
//    }
//        private void OnCollisionEnter(Collision collision)
//    {
//        if (enableMovementOnNextTouch)
//        {
//            enableMovementOnNextTouch = false;
//            ResetRestrictions();

           
//        }
//    }
//}