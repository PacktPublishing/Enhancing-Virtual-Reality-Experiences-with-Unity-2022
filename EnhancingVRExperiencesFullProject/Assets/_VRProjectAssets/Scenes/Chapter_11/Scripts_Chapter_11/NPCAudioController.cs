using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]


public class NPCAudioController : MonoBehaviour
{
    public AudioClip[] audioClips; // array of audio clips to randomly play
    public float minTimeBetweenSounds = 5f; // minimum time between playing sounds
    public float maxTimeBetweenSounds = 10f; // maximum time between playing sounds

    private float timeSinceLastSound; // time since last sound was played

    // Start is called before the first frame update
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {


        // play a random audio clip with a random chance
        if (Time.deltaTime - timeSinceLastSound > Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds))
        {
            int clipIndex = Random.Range(0, audioClips.Length);
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = audioClips[clipIndex];
            audioSource.Play();
            timeSinceLastSound = Time.deltaTime;
        }


    }

}
