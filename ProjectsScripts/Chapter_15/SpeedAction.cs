using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Modifies the speed of a ContinuousMoveProviderBase for speed boosting.
public class SpeedAction : MonoBehaviour
{
    // The ContinuousMoveProviderBase component to modify
    public ContinuousMoveProviderBase moveProvider;

    // The speed boost amount
    public float speedBoost;

    // Indicates whether the super speed mode is active
    public bool superSpeed;

    // The current speed of the move provider
    private float currentSpeed;

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
            moveProvider.moveSpeed = speedBoost;
        }
        else
        {
            moveProvider.moveSpeed = currentSpeed;
        }
    }

    // Activate the super speed mode
    public void SpeedOn()
    {
        superSpeed = true;
    }

    // Deactivate the super speed mode
    public void SpeedOff()
    {
        superSpeed = false;
    }
}
