using System.Collections;
using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    // Thresholds for good and bad FPS
    const float goodFpsThreshold = 72;
    const float badFpsThreshold = 50;

    // Interval at which to update the FPS display
    public float updateInteval = 0.5f;

    private TextMeshProUGUI textOutput = null;

    private float deltaTime = 0.0f;
    private float milliseconds = 0.0f;
    private int framesPerSecond = 0;

    private void Awake()
    {
        // Get the TextMeshProUGUI component for displaying the FPS
        textOutput = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        // Start the coroutine to show the FPS
        StartCoroutine(ShowFPS());
    }

    private void Update()
    {
        // Calculate the current FPS
        CalculateCurrentFPS();
    }

    private void CalculateCurrentFPS()
    {
        // Calculate the delta time and milliseconds
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        milliseconds = (deltaTime * 1000.0f);
        framesPerSecond = (int)(1.0f / deltaTime);
    }

    private IEnumerator ShowFPS()
    {
        while (true)
        {
            // Set the color of the FPS display based on the FPS value
            if (framesPerSecond >= goodFpsThreshold)
            {
                textOutput.color = Color.green;
            }
            else if (framesPerSecond >= badFpsThreshold)
            {
                textOutput.color = Color.yellow;
            }
            else
            {
                textOutput.color = Color.red;
            }

            // Update the text of the FPS display
            textOutput.text = "FPS: " + framesPerSecond + "\n" + "MS: " + milliseconds.ToString(".0");

            // Wait for the specified update interval
            yield return new WaitForSeconds(updateInteval);
        }
    }
}
