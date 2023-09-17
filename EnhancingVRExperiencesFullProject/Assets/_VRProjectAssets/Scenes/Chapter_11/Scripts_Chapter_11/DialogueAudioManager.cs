using System.Collections.Generic;
using UnityEngine;

public class DialogueAudioManager : MonoBehaviour
{
    public AudioSource audioSource;         // The AudioSource for playing audio clips
    public List<AudioClip> angryClips;      // List of angry audio clips
    public List<AudioClip> answerClips;
    public List<AudioClip> braveClips;
    public List<AudioClip> carefulClips;
    public List<AudioClip> excitedClips;    // List of shocked audio clips
    public List<AudioClip> happyClips;     // List of casual audio clips
    public List<AudioClip> questionClips;       // List of loud audio clips
    public List<AudioClip> sadClips;
    public List<AudioClip> scaredClips;
    public bool playAngryClips = false;      // Whether to play angry clips on startup
    public bool playAnswerClips = false;   // Whether to play shocked clips on startup
    public bool playBraveClips = false;    // Whether to play casual clips on startup
    public bool playCarefulClips = false;      // Whether to play loud clips on startup
    public bool playExcitedClips = false;
    public bool playHappyClips = false;
    public bool playQuestionClips = false;
    public bool playSadClips = false;
    public bool playScaredClips = false;
    private List<AudioClip>[] clipLists;    // Array of clip lists for different categories
    private List<AudioClip> currentList;    // The current list of audio clips being played
    private int currentIndex;               // Index of the current audio clip being played

    void Start()
    {
        int picker = Random.Range(1, 9);
        if (picker == 1)
        {
            playAngryClips = true;
        }
        else if (picker == 2)
        {
            playAnswerClips = true;
        }
        else if (picker == 3)
        {
            playBraveClips = true;
        }
        else if (picker == 4)
        {
            playCarefulClips = true;
        }
        else if (picker == 5)
        {
            playExcitedClips = true;
        }
        else if (picker == 6)
        {
            playHappyClips = true;
        }
        else if (picker == 7)
        {
            playQuestionClips = true;
        }
        else if (picker == 8)
        {
            playSadClips = true;
        }
        else if (picker == 9)
        {
            playScaredClips = true;
        }
        // Initialize the array of clip lists
        clipLists = new List<AudioClip>[] { angryClips, answerClips, braveClips, carefulClips, excitedClips, happyClips, questionClips, sadClips, scaredClips };

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