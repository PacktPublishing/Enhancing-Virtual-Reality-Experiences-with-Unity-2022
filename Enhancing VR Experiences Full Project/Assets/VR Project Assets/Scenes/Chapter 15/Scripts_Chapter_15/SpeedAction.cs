using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpeedAction : MonoBehaviour
{
    public ContinuousMoveProviderBase moveProvider;
    public float speedBoost;
    public bool superSpeed;
    public float currentSpeed;

 

    // Start is called before the first frame update
    void Start()
    {
        moveProvider = FindObjectOfType<ContinuousMoveProviderBase>();
        currentSpeed = moveProvider.moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (superSpeed)
        {
            moveProvider.moveSpeed = currentSpeed * speedBoost;
        }
        else
        {
           moveProvider.moveSpeed = currentSpeed;
        }
    }
    public void SpeedOn()
    {
        superSpeed = true;
    }
    public void SpeedOff()
    {
        superSpeed = false;
    }
}
