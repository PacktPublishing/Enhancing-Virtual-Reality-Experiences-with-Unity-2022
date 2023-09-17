using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class VRGrenade : MonoBehaviour
{
    // The sound to play when the grenade explodes
    public AudioClip explosionSound;

    // The particles to instantiate when the grenade explodes
    public GameObject explosionParticles;

    // The time it takes for the grenade to arm itself
    public float armTime = 4f;

    // The radius of the explosion
    public float explosionRadius = 5f;

    // The force of the explosion
    public float explosionForce = 500f;

    // The proximity indicator for the grenade
    public GameObject proximityIndicator;

    // The sound to play when the player is near the grenade
    public AudioClip proximitySound;

    // The starting distance at which the proximity sound is audible
    public float proximitySoundStartDistance = 2f;

    // The maximum distance at which the proximity sound is audible
    public float proximitySoundMaxDistance = 5f;

    private Rigidbody rigidbody;
    public bool isArmed = false;
    private float armTimer = 0f;
    private AudioSource audioSource;
    private bool isPlayingProximitySound = false;
    internal static Action OnRemoved;

    void Start()
    {
        // Get the Rigidbody component
        rigidbody = GetComponent<Rigidbody>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // If the grenade is armed, start the arm timer
        if (isArmed)
        {
            armTimer += Time.deltaTime;

            // If the arm timer has reached the arm time, explode the grenade
            if (armTimer >= armTime)
            {
                Explode();
            }

            // Update the proximity indicator and sound
            UpdateProximityIndicator();
            UpdateProximitySound();
        }
    }

    public void Arm()
    {
        isArmed = true;
    }

    public void Throw(Vector3 throwVelocity)
    {
        // Enable the Rigidbody and apply the throw velocity
        rigidbody.isKinematic = false;
        rigidbody.velocity = throwVelocity;

        // Unparent the grenade from the hand
        transform.parent = null;

        // Arm the grenade
        Arm();
    }

    void Explode()
    {
        // Play the explosion sound
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        // Instantiate the explosion particles
        GameObject explode = Instantiate(explosionParticles, transform.position, Quaternion.identity);

        // Find all colliders within the explosion radius and apply a force to them
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody otherRigidbody = collider.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                otherRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        // Destroy the grenade and the explosion particles
        Destroy(gameObject, 1f);
        Destroy(explode, 1f);
    }

    void UpdateProximityIndicator()
    {
        // Calculate the progress of the arm timer as a percentage
        float progress = Mathf.Clamp01(armTimer / armTime);

        // Calculate the blink interval based on the progress
        float blinkInterval = Mathf.Lerp(1f, 0.1f, progress);
        // Increase the blink interval as the time gets closer to the end
        blinkInterval *= Mathf.Lerp(1f, 2f, progress);

        // If the proximity indicator is active, toggle it off and on with the blink interval
        if (proximityIndicator.activeSelf)
        {
            proximityIndicator.SetActive(false);
            Invoke("ToggleProximityIndicator", blinkInterval);
        }
        else
        {
            proximityIndicator.SetActive(true);
            Invoke("ToggleProximityIndicator", blinkInterval);
        }
    }

    void ToggleProximityIndicator()
    {
        // Toggle the proximity indicator on and off
        proximityIndicator.SetActive(!proximityIndicator.activeSelf);
    }

    void UpdateProximitySound()
    {
        // Calculate the distance between the grenade and the player
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        // If the distance is less than the max distance, play the proximity sound
        if (distance < proximitySoundMaxDistance)
        {
            // Calculate the pitch of the sound based on the distance
            float pitch = Mathf.Lerp(1f, 2f, 1f - (distance - proximitySoundStartDistance) / (proximitySoundMaxDistance - proximitySoundStartDistance));
            audioSource.pitch = pitch;

            // If the sound is not already playing, start playing it
            if (!isPlayingProximitySound)
            {
                audioSource.clip = proximitySound;
                audioSource.loop = true;
                audioSource.Play();
                isPlayingProximitySound = true;
            }
        }
        else
        {
            // If the sound is playing, stop it
            if (isPlayingProximitySound)
            {
                audioSource.Stop();
                isPlayingProximitySound = false;
            }
        }

        // If the grenade is armed, increase the frequency of the proximity sound as the time gets closer to the end
        if (isArmed)
        {
            // Calculate the progress of the arm timer as a percentage
            float progress = Mathf.Clamp01(armTimer / armTime);

            // Increase the frequency of the proximity sound as the time gets closer to the end
            float pitchMultiplier = Mathf.Lerp(1f, 2f, progress);
            audioSource.pitch = pitchMultiplier;

            // Calculate the number of times to play the proximity sound based on the progress
            int numPlays = 0;
            if (progress >= 0.1f)
            {
                numPlays = 1;
            }
            if (progress >= 0.3f)
            {
                numPlays = 2;
            }
            if (progress >= 0.6f)
            {
                numPlays = 3;
            }
            if (progress >= 0.75f)
            {
                numPlays = 4;
            }

            // Play the proximity sound the specified number of times
            for (int i = 0; i < numPlays; i++)
            {
                AudioSource.PlayClipAtPoint(proximitySound, transform.position);
            }
        }
    }
}
