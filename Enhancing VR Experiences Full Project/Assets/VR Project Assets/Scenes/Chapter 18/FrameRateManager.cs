using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class FrameRateManager : MonoBehaviour
{
    [Header("Frame Settings")]
    int MaxRate = 9999;
    public float targetFrameRate = 90.0f;
    float currentFrameRate;

    public float updateDelay = 0f;

    private float _deltaTime = 0f;

    private TextMeshProUGUI _textFPS;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Set vSyncCount to 0 to allow the frame rate to be determined by targetFrameRate
        QualitySettings.vSyncCount = 0;

        // Set the application's target frame rate to a high value
        UnityEngine.Application.targetFrameRate = MaxRate;

        // Initialize the current frame rate to the current time
        currentFrameRate = Time.realtimeSinceStartup;

        // Start the coroutine to wait for the next frame
        StartCoroutine(WaitForNextFrame());
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the TextMeshProUGUI component attached to this GameObject
        _textFPS = GetComponent<TextMeshProUGUI>();

        // Start the coroutine to display the frames per second
        StartCoroutine(DisplayFramesPerSecond());
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the current frames per second
        GenerateFramesPerSecond();
    }

    // Calculate the current frames per second
    private void GenerateFramesPerSecond()
    {
        // Calculate the smoothed delta time
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * .1f;

        // Calculate the current frames per second
        currentFrameRate = 1.0f / _deltaTime;
    }

    // Coroutine to display the frames per second
    private IEnumerator DisplayFramesPerSecond()
    {
        while (true)
        {
            // Set the color of the text based on the current frames per second
            if (currentFrameRate >= targetFrameRate)
            {
                _textFPS.color = new Color32(0, 177, 215, 255); // Green color
            }
            else
            {
                _textFPS.color = new Color32(200, 68, 124, 255); // Red color
            }

            // Update the text to display the frames per second
            _textFPS.text = "FPS: " + currentFrameRate.ToString(".0");

            // Wait for the specified update delay before updating the text again
            yield return new WaitForSeconds(updateDelay);
        }
    }

    // Coroutine to wait for the next frame
    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            // Wait for the end of the current frame
            yield return new WaitForEndOfFrame();

            // Calculate the time for the next frame
            currentFrameRate += 1.0f / targetFrameRate;

            // Calculate the sleep time based on the current frame rate
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameRate - t - 0.01f;

            // If the sleep time is positive, sleep for that duration
            if (sleepTime > 0)
                Thread.Sleep((int)(sleepTime * 1000));

            // Wait until the next frame time is reached
            while (t < currentFrameRate)
                t = Time.realtimeSinceStartup;
        }
    }
}
