using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class GravityManager : MonoBehaviour
{

    public GravityOrbit Gravity;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Gravity)
        {
            Vector3 gravityUp = Vector3.zero;
            if (Gravity.fixedDirection)
            {
                gravityUp = Gravity.transform.up;
            }
            else
            {
                gravityUp = (transform.position - Gravity.transform.position).normalized;
            }

            Vector3 localUp = transform.up;
            Quaternion targetrotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
            transform.up = Vector3.Lerp(transform.up, gravityUp, 20 * Time.deltaTime);
            rb.AddForce(Gravity.GetGravityForce(rb.mass, transform.position));
        }
    }

    private Vector3 GetGravityUpDirection()
    {
        Vector3 gravityUp = Vector3.zero;

        if (Gravity)
        {
            if (Gravity.fixedDirection)
            {
                gravityUp = Gravity.transform.up;
            }
            else
            {
                // Get a list of all active GravityChanger objects in the scene
                GravityChanger[] gravityChangers = FindObjectsOfType<GravityChanger>();

                // Find the nearest active GravityChanger to the player
                float nearestDistance = Mathf.Infinity;
                GravityChanger nearestChanger = null;

                foreach (GravityChanger changer in gravityChangers)
                {
                    if (!changer.gameObject.activeSelf) continue;

                    float distance = Vector3.Distance(transform.position, changer.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestChanger = changer;
                    }
                }

                // Calculate the gravity up direction based on the nearest GravityChanger
                if (nearestChanger != null)
                {
                    gravityUp = (transform.position - nearestChanger.transform.position).normalized;
                }
            }
        }

        return gravityUp;
    }



    //Take 3

    //public GravityOrbit Gravity;
    //private Rigidbody rb;
    //public float RotationSpeed = 20;

    //public XROrigin xrOrigin;
    //public Vector3 gravityUp;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    xrOrigin = FindObjectOfType<XROrigin>();
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (Gravity)
    //    {
    //        Vector3 gravityUp = Vector3.zero;
    //        if (Gravity.fixedDirection)
    //        {
    //            gravityUp = Gravity.transform.up;
    //        }
    //        else
    //        {
    //            // Get the XROrigin from the XR device


    //                // Calculate gravity up direction based on the position of the XROrigin
    //                gravityUp = (transform.position - xrOrigin.transform.position).normalized;

    //        }

    //        Vector3 localUp = transform.up;
    //        Quaternion targetrotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
    //        transform.up = Vector3.Lerp(transform.up, gravityUp, RotationSpeed * Time.deltaTime);
    //        rb.AddForce((-gravityUp * Gravity.gravity) * rb.mass);
    //    }
    //}




    //take 2

    //public GravityOrbit Gravity;
    //private Rigidbody rb;
    //public float RotationSpeed = 20;


    //// Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    if(Gravity)
    //    {
    //        Vector3 gravityUp = Vector3.zero;
    //        if (Gravity.FixedDirection)
    //        {
    //            gravityUp = Gravity.transform.up;
    //        }
    //        else
    //        {
    //            gravityUp = (transform.position - Gravity.transform.position).normalized;
    //        }

    //        Vector3 localUp = transform.up;
    //        Quaternion targetrotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;
    //        transform.up = Vector3.Lerp(transform.up, gravityUp, RotationSpeed * Time.deltaTime);
    //        rb.AddForce((-gravityUp * Gravity.Gravity) * rb.mass);
    //    }
    //}
}
