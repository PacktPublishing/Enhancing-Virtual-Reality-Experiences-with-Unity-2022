using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DialogueAudioManager : MonoBehaviour
{
    // The AudioSource for playing audio clips
    public AudioSource audioSource;

    // List of audio clips for the angry category
    public List<AudioClip> angryClips;

    // List of audio clips for the answer category
    public List<AudioClip> answerClips;

    // List of audio clips for the brave category
    public List<AudioClip> braveClips;

    // List of audio clips for the careful category
    public List<AudioClip> carefulClips;

    // List of audio clips for the excited category
    public List<AudioClip> excitedClips;

    // List of audio clips for the happy category
    public List<AudioClip> happyClips;

    // List of audio clips for the question category
    public List<AudioClip> questionClips;

    // List of audio clips for the sad category
    public List<AudioClip> sadClips;

    // List of audio clips for the scared category
    public List<AudioClip> scaredClips;

    // Whether to play angry clips on startup
    public bool playAngryClips = false;

    // Whether to play answer clips on startup
    public bool playAnswerClips = false;

    // Whether to play brave clips on startup
    public bool playBraveClips = false;

    // Whether to play careful clips on startup
    public bool playCarefulClips = false;

    // Whether to play excited clips on startup
    public bool playExcitedClips = false;

    // Whether to play happy clips on startup
    public bool playHappyClips = false;

    // Whether to play question clips on startup
    public bool playQuestionClips = false;

    // Whether to play sad clips on startup
    public bool playSadClips = false;

    // Whether to play scared clips on startup
    public bool playScaredClips = false;

    // Array of clip lists for different categories
    private List<AudioClip>[] clipLists;

    // The current list of audio clips being played
    private List<AudioClip> currentList;

    // Index of the current audio clip being played
    private int currentIndex;               

    void Start()
    {
        // Randomly choose a number between 1 and 9 to determine which category to play clips from
        int picker = Random.Range(1, 10);

        // Set the corresponding boolean based on the chosen category
        switch (picker)
        {
            case 1:
                playAngryClips = true;
                break;
            case 2:
                playAnswerClips = true;
                break;
            case 3:
                playBraveClips = true;
                break;
            case 4:
                playCarefulClips = true;
                break;
            case 5:
                playExcitedClips = true;
                break;
            case 6:
                playHappyClips = true;
                break;
            case 7:
                playQuestionClips = true;
                break;
            case 8:
                playSadClips = true;
                break;
            case 9:
                playScaredClips = true;
                break;
        }

        // Initialize the array of clip lists
        clipLists = new List<AudioClip>[]
        {
            angryClips,
            answerClips,
            braveClips,
            carefulClips,
            excitedClips,
            happyClips,
            questionClips,
            sadClips,
            scaredClips
        };

        // Determine which list to play clips from based on the startup booleans
        if (playAngryClips)
        {
            currentList = angryClips;
        }
        else if (playAnswerClips)
        {
            currentList = answerClips;
        }
        else if (playBraveClips)
        {
            currentList = braveClips;
        }
        else if (playCarefulClips)
        {
            currentList = carefulClips;
        }
        else if (playExcitedClips)
        {
            currentList = excitedClips;
        }
        else if (playHappyClips)
        {
            currentList = happyClips;
        }
        else if (playQuestionClips)
        {
            currentList = questionClips;
        }
        else if (playSadClips)
        {
            currentList = sadClips;
        }
        else if (playScaredClips)
        {
            currentList = scaredClips;
        }

        // Randomly select an audio clip from the current list and play it
        currentIndex = Random.Range(0, currentList.Count);
        audioSource.clip = currentList[currentIndex];
        audioSource.Play();
    }

    void Update()
    {
        // If the audio clip has finished playing, select a new one from the current list
        if (!audioSource.isPlaying)
        {
            currentIndex = Random.Range(0, currentList.Count);
            audioSource.clip = currentList[currentIndex];
            audioSource.Play();
        }
    }

    // Set the current list of audio clips based on the given category
    public void SetClipList(string category)
    {
        switch (category)
        {
            case "angry":
                currentList = angryClips;
                break;
            case "answer":
                currentList = answerClips;
                break;
            case "brave":
                currentList = braveClips;
                break;
            case "careful":
                currentList = carefulClips;
                break;
            case "excited":
                currentList = excitedClips;
                break;
            case "happy":
                currentList = happyClips;
                break;
            case "question":
                currentList = questionClips;
                break;
            case "sad":
                currentList = sadClips;
                break;
            case "scared":
                currentList = scaredClips;
                break;
            default:
                Debug.LogError("Invalid clip category: " + category);
                break;
        }
    }
}
