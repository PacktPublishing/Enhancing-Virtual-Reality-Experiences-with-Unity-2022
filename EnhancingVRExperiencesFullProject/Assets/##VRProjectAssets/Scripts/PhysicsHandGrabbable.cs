using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsHandGrabbable : MonoBehaviour
    {

public InputActionProperty grabInputSource;
public float radius = 0.1f;
public LayerMask grabLayer;

private FixedJoint fixedJoint;
private bool isGrabbing = false;

private void FixedUpdate() {
    bool isGrabButtonPRessed = grabInputSource.action.ReadValue<float>()>0.1f;
    if(isGrabButtonPRessed&&isGrabbing){
        Collider[] nearbyCollider = Physics.OverlapSphere(transform.position,radius,grabLayer,QueryTriggerInteraction.Ignore);
        if(nearbyCollider.Length>0){
                Rigidbody nearRigidbody = nearbyCollider[0].attachedRigidbody;
            fixedJoint=gameObject.AddComponent<FixedJoint>();
                fixedJoint.autoConfigureConnectedAnchor = false;
            if(nearRigidbody){
                fixedJoint.connectedAnchor=nearRigidbody.transform.InverseTransformPoint(transform.position);

            }
            else{
                fixedJoint.connectedAnchor=transform.position;
            }
        }
    }
}










    }