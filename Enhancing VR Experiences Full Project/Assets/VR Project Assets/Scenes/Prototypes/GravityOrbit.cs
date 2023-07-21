using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class GravityOrbit : MonoBehaviour
{

    public float gravity;
    public bool fixedDirection;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GravityManager gravityManager = other.GetComponent<GravityManager>();
            if (gravityManager)
            {
                gravityManager.Gravity = this;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GravityManager gravityManager = other.GetComponent<GravityManager>();
            if (gravityManager)
            {
                gravityManager.Gravity = null;
            }
        }
    }

    public Vector3 GetGravityForce(float mass, Vector3 position)
    {
        if (fixedDirection)
        {
            return (-transform.up * gravity) * mass;
        }
        else
        {
            return (-((position - transform.position).normalized) * gravity) * mass;
        }
    }



    //Take 2


    //   public float gravity;
    //   public bool fixedDirection;

    //   private Transform xrRigTransform;

    //   public XROrigin xrOrigin;


    //   private void Start()
    //   {
    //xrOrigin = FindObjectOfType<XROrigin>();
    //   }

    //   private void OnTriggerStay(Collider other)
    //   {
    //       if (other.CompareTag("Player"))
    //       {
    //           GravityManager gravityManager = other.GetComponent<GravityManager>();
    //           if (gravityManager)
    //           {
    //               gravityManager.Gravity = this;
    //               gravityManager.gravityUp = GetGravityUpDirection();
    //           }
    //       }
    //   }

    //   private void OnTriggerExit(Collider other)
    //   {
    //       if (other.CompareTag("Player"))
    //       {
    //           GravityManager gravityManager = other.GetComponent<GravityManager>();
    //           if (gravityManager)
    //           {
    //               gravityManager.Gravity = null;
    //           }
    //       }
    //   }

    //   private Vector3 GetGravityUpDirection()
    //   {
    //       if (fixedDirection)
    //       {
    //           return transform.up;
    //       }
    //       else
    //       {
    //           return (xrRigTransform.position - transform.position).normalized;
    //       }
    //   }

    //   public Vector3 GetGravityForce(float mass)
    //   {
    //       return (-GetGravityUpDirection() * gravity) * mass;
    //   }

    //take 1

    //public float Gravity;
    //public bool FixedDirection;
    //public bool rbGravityEnter;
    //public bool rbGravityExit;
    //private void OnTriggerEnter(Collider other)
    //{if(other.CompareTag("Player"))
    //    if (rbGravityExit&&other.CompareTag("Player"))
    //    {
    //        other.attachedRigidbody.useGravity = true;
    //    }
    //    else
    //    {
    //        other.attachedRigidbody.useGravity = false;
    //    }
    //    if (other.GetComponent<GravityManager>())
    //    {

    //        other.GetComponent<GravityManager>().Gravity = this.GetComponent<GravityOrbit>();
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //        if (rbGravityEnter)
    //    {
    //        other.attachedRigidbody.useGravity = true;
    //    }
    //    else
    //    {
    //        other.attachedRigidbody.useGravity = false;
    //    }


    //}

}
