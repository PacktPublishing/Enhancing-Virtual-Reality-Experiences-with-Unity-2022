using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class DisplayFrameRateManager : MonoBehaviour
{
    [Header("Frame Settings")]
    int MaxRate = 9999;
    public float targetFrameRate = 90.0f;
    float currentFrameRate;

    public float updateDelay = 0f;

    private float _deltaTime = 0f;

    private TextMeshProUGUI _textFPS;

    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = MaxRate;
        currentFrameRate = Time.realtimeSinceStartup;
        StartCoroutine("WaitForNextFrame");
    }

    // Start is called before the first frame update
    void Start()
    {
        _textFPS = GetComponent<TextMeshProUGUI>();
        StartCoroutine(DisplayFramesPerSecond());
    }

    // Update is called once per frame
    void Update()
    {

            GenerateFramesPerSecond();
    }

    private void GenerateFramesPerSecond()
    {
        _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * .1f;
        currentFrameRate = 1.0f / _deltaTime;
    }

    private IEnumerator DisplayFramesPerSecond()
    {
        while (true)
        {
            if (currentFrameRate >= targetFrameRate)
            {
                _textFPS.color = new Color32(0, 177, 215, 255);
            }
            else
            {
                _textFPS.color = new Color32(200, 68, 124, 255);
            }
            _textFPS.text = "FPS: " + currentFrameRate.ToString(".0");
            yield return new WaitForSeconds(updateDelay);
        }

    }

    IEnumerator WaitForNextFrame()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            currentFrameRate += 1.0f / targetFrameRate;
            var t = Time.realtimeSinceStartup;
            var sleepTime = currentFrameRate - t - 0.01f;
            if (sleepTime > 0)
                Thread.Sleep((int)(sleepTime * 1000));
            while (t < currentFrameRate)
                t = Time.realtimeSinceStartup;
        }
    }
}
