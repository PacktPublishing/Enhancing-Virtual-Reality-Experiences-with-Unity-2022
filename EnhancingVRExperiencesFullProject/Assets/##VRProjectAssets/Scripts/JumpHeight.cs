using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpHeight : MonoBehaviour
    {
public float jumpHeight;
public float jumpVelocity;
public Rigidbody rb;
public Rigidbody leftHandRB;
public Rigidbody rightHandRB;
public bool jumpWithHand = true;
public float minJumpWithHandSpeed = 2;
public float maxJumpWithHandSpeed = 7;
public bool isGrounded;
    public InputActionReference jumpInputSource;

public void Update(){

    bool jumpInput = jumpInputSource.action.WasPressedThisFrame();
    if(!jumpWithHand){
if(jumpInput && isGrounded){
jumpVelocity = Mathf.Sqrt(2*-Physics.gravity.y*jumpHeight);
rb.velocity = Vector3.up * jumpVelocity;
    }
    }
    else
    {
        bool inpoutJumpPressed = jumpInputSource.action.IsPressed();
        float handSpeed = ((leftHandRB.velocity - rb.velocity).magnitude + (rightHandRB.velocity-rb.velocity).magnitude)/2;
        if(inpoutJumpPressed&&isGrounded&&handSpeed>minJumpWithHandSpeed){
            rb.velocity=Vector3.up*Mathf.Clamp(handSpeed,minJumpWithHandSpeed,maxJumpWithHandSpeed);
        }
    }
    
}


    }
