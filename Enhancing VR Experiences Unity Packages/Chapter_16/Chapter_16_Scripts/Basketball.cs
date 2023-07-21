using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Basketball : MonoBehaviour
{
    // The multiplier to apply when dribbling the basketball
    public float forceMultiplier = 10f;

    // The maximum velocity the basketball can have
    public float maxVelocity = 10f;

    // The threshold velocity below which the basketball can be dribbled
    public float dribbleThreshold = 0.1f;

    // The threshold velocity for bouncing the basketball
    public float bounceThreshold = 0.2f;

    // The multiplier to apply when bouncing the basketball
    public float bounceMultiplier = 0.8f;

    // The sound to play when the basketball bounces
    public AudioClip bounceSound;

    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;
    private AudioSource audioSource;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (grabInteractable.isSelected)
        {
            // Dribble the basketball
            Vector3 velocity = rb.velocity;
            float verticalVelocity = velocity.y;
            float horizontalVelocity = Mathf.Sqrt(velocity.x * velocity.x + velocity.z * velocity.z);
            float speed = Mathf.Sqrt(horizontalVelocity * horizontalVelocity + verticalVelocity * verticalVelocity);
            if (speed < dribbleThreshold)
            {
                rb.AddForce(transform.up * forceMultiplier, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Bounce the basketball
        float force = collision.relativeVelocity.magnitude * bounceMultiplier;
        if (force > bounceThreshold)
        {
            rb.AddForce(collision.contacts[0].normal * force, ForceMode.Impulse);
            audioSource.PlayOneShot(bounceSound);
        }
    }

    private void FixedUpdate()
    {
        // Limit the velocity of the basketball
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }
}
