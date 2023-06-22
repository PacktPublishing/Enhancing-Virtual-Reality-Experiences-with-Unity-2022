using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiAudioSourceManager : MonoBehaviour
{
    // The audio clip to play
    public AudioClip audioClip;

    // The list of AudioSources to play the clip on
    public List<AudioSource> audioSources;

    void Start()
    {
        // Set the clip for each AudioSource and set spatialBlend to 1
        foreach (AudioSource source in audioSources)
        {
            source.clip = audioClip;
            source.spatialBlend = 1f;
        }

        // Play the audio clip on each AudioSource
        Play();
    }

    // Play the clip on each AudioSource in the list
    public void Play()
    {
        foreach (AudioSource source in audioSources)
        {
            source.Play();
        }
    }

    // Stop playing the clip on each AudioSource in the list
    public void Stop()
    {
        foreach (AudioSource source in audioSources)
        {
            source.Stop();
        }
    }
}
