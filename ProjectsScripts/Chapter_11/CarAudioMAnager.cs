using System;
using UnityEngine;

public class CarAudioMAnager : MonoBehaviour
{
    // The AudioSource for the engine sound
    public AudioSource engineAudioSource;

    // The AudioSource for the honk sound
    public AudioSource honkAudioSource;

    // The audio clip for the engine sound
    public AudioClip engineSound;

    // The audio clip for the honk sound
    public AudioClip honkSound;

    // The minimum time between honks (in seconds)
    public float minHonkInterval = 10f;

    // The maximum time between honks (in seconds)
    public float maxHonkInterval = 120f;

    // The cooldown period between honks (in seconds)
    public float honkCooldown = 120f;

    // Timer for tracking time until next honk
    public float honkTimer = 0f;

    // Timer for tracking cooldown period after honk
    private float cooldownTimer = 0f;

    // Flag indicating whether to play the honk sound
    public bool playHonk;

    void Start()
    {
        // Set the engine sound clip and start playing the audio source
        engineAudioSource.clip = engineSound;
        engineAudioSource.loop = true;
        engineAudioSource.Play();

        // Set the honk sound clip
        honkAudioSource.clip = honkSound;
    }

    void Update()
    {
        // If the honk timer is less than or equal to zero, play the honk sound
        if (honkTimer <= 0f)
        {
            // Set the honk timer to a random value within the honking window
            honkTimer = Random.Range(minHonkInterval, maxHonkInterval);

            // Determine if it is time to play the honk sound based on the position of the timer within the honking window
            var honkplayer = maxHonkInterval / 2;
            if (honkTimer > honkplayer)
            {
                playHonk = true;
            }

            // If the honk audio source is not currently playing, play the honk sound
            if (!honkAudioSource.isPlaying && playHonk)
            {
                honkAudioSource.Play();
                playHonk = false;
            }
        }
        else
        {
            honkTimer -= Time.deltaTime;
        }

        // If the honk audio source is playing, decrement the cooldown timer
        if (honkAudioSource.isPlaying)
        {
            cooldownTimer -= Time.deltaTime;

            // If the cooldown timer has reached zero, reset the honk timer and cooldown timer
            if (cooldownTimer <= 0f)
            {
                honkTimer = Random.Range(minHonkInterval, maxHonkInterval);
                cooldownTimer = honkCooldown;
            }
        }
    }
}
