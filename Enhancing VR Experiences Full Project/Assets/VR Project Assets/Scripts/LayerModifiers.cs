
//using UnityEngine;
//using System.Collections;

//public class LayerModifiers : MonoBehaviour
//{
//    [TagSelector]
//    public string PlayerTag;
//    public float layerGravity;
//    private float currentGravity;
//    public float layerFriction;
//    private float currentFriction;
//    private float layerMoveSpeed;
//    private float currentMoveSpeed;
//    private float layerJumpForce;
//    private float currentJumpForce;

//    [TagSelector]
//    public string groundTag;
//    public LayerMask groundLayer;

//    [System.Serializable]
//    public class SurfaceModifiers
//    {
//        public LayerMask layer;
//        public float moveSpeed;
//        public float jumpForce;
//    }

//    public SurfaceModifiers[] surfaceModifiers;

//    public int currentGroundLayer { get; private set; }

//    public string currentGroundLayerTag
//    {
//        get { return currentGroundLayer != -1 ? LayerMask.LayerToName(currentGroundLayer) : ""; }
//    }

//    private CrouchingAction crouchingAction;
//    private JumpingAction jumpingAction;
//    private SpeedAction speedAction;

//    private void Awake()
//    {
//        crouchingAction = GetComponent<CrouchingAction>();
//        jumpingAction = GetComponent<JumpingAction>();
//        speedAction = GetComponent<SpeedAction>();
//    }

//    private void Start()
//    {
//        ResetValues();
//    }

//    private void ResetValues()
//    {
//        currentFriction = layerFriction;
//        currentGravity = layerGravity;
//        currentJumpForce = layerJumpForce;
//        currentMoveSpeed = layerMoveSpeed;
//    }

//    private void Update()
//    {
//        if (currentGroundLayer == -1)
//        {
//            // Player is not on the ground
//            ResetValues();
//        }
//    }

//    private void OnControllerColliderHit(ControllerColliderHit hit)
//    {
//        if (hit.gameObject.CompareTag(groundTag))
//        {
//            // Check if the collision layer matches the groundLayer
//            if (groundLayer == (groundLayer | (1 << hit.gameObject.layer)))
//            {
//                if (currentGroundLayer != hit.gameObject.layer)
//                {
//                    // Player has landed on a new ground layer
//                    currentGroundLayer = hit.gameObject.layer;
//                    foreach (var modifier in surfaceModifiers)
//                    {
//                        if (modifier.layer == (modifier.layer | (1 << hit.gameObject.layer)))
//                        {
//                            // Apply surface modifiers
//                            currentMoveSpeed = modifier.moveSpeed;
//                            currentJumpForce = modifier.jumpForce;
//                            break;
//                        }
//                    }
//                }
//            }
//            else if (currentGroundLayer == hit.gameObject.layer)
//            {
//                // Player has left the ground
//                currentGroundLayer = -1;
//                ResetValues();
//            }
//        }
//    }

//    // These methods should be replaced with your own implementation of player movement
//    private float moveSpeed { get { return currentMoveSpeed; } set { currentMoveSpeed = value; } }
//    private float jumpForce { get { return currentJumpForce; } set { currentJumpForce = value; } }
//}





////using UnityEngine;
////using System.Collections;

////public class LayerModifiers : MonoBehaviour
////{
////    [TagSelector]
////    public string PlayerTag;
////    public float layerGravity; // reference the rigidbody from player controller
////    private float currentGravity;
////    public float layerFriction;
////    private float currentFriction;
////    private float layerMoveSpeed;
//// [TagSelector]
////    public string SpeedBoostModifierTag;
////    public LayerMask SpeedBoostModifierLayer;
////    public float SpeedBoostSpeed;
////    private float currentMoveSpeed;
////    [TagSelector]
////    public string JumpModifierTag;
////    public LayerMask JumpModifierLayer;
////    public float jumpForceBoost;
////    private float layerJumpForce;
////    private float currentJumpForce;
////    [TagSelector]
////    public string SpeedDropModifierTag;
////    public LayerMask SpeedDropModifierLayer;
////    public float SpeedDropSpeed;
////    [TagSelector]
////    public string groundTag;
////    public LayerMask groundLayer;
////    public float coolDownTime;
////    private float coolDown;

////    private CrouchingAction crouchingAction;
////    private JumpingAction jumpingAction;
////    private SpeedAction speedAction;

////    private void Awake()
////    {
////        crouchingAction = GetComponent<CrouchingAction>();
////        jumpingAction = GetComponent<JumpingAction>();
////        speedAction = GetComponent<SpeedAction>();

////    }

////    private void Start()
////    {
////        ResetValues();
////    }

////    private void ResetValues()
////    {
////        currentFriction = layerFriction;
////        currentGravity = layerGravity;
////        currentJumpForce = layerJumpForce;
////        currentMoveSpeed = layerMoveSpeed;
////    }

////    private void Update()
////    {
////        if (coolDown == 0)
////        {
////            ResetValues();
////        }
////        else if (coolDown > 0)
////        {
////            coolDown -= Time.deltaTime;
////        }
////    }

////    private void OnControllerColliderHit(ControllerColliderHit hit)
////    {
////        switch (hit.gameObject.tag)
////        {
////            case SpeedBoostModifierTag:
////                layerMoveSpeed = moveSpeed * SpeedBoostSpeed;
////                speedAction.moveProvider.moveSpeed =layerMoveSpeed;
////                StartCoroutine(CoolDownDuration());
////                break;
////            case JumpModifierTag:
////                layerJumpForce = jumpForce * jumpForceBoost;
////                jumpingAction.jumpForce = layerJumpForce;
////                StartCoroutine(CoolDownDuration());
////                break;
////            case SpeedDropModifierLayer:
////                layerMoveSpeed = moveSpeed / SpeedDropSpeed;
////                speedAction.moveProvider.moveSpeed = layerMoveSpeed;
////                StartCoroutine(CoolDownDuration());
////                break;
////            default:
////                if (groundLayer == (groundLayer | (1 << hit.gameObject.layer))) // Check if the collision layer matches the groundLayer
////                {
////                    moveSpeed = currentMoveSpeed;
////                    jumpForce = currentJumpForce;
////                    StartCoroutine(CoolDownDuration());
////                }
////                break;
////        }
////    }

////    IEnumerator CoolDownDuration()
////    {
////        coolDown = coolDownTime;
////        yield return new WaitForSeconds(coolDownTime);
////        coolDown = 0;
////    }

////    // These methods should be replaced with your own implementation of player movement
////    private float moveSpeed { get; set; }
////    private float jumpForce { get; set; }
////}

