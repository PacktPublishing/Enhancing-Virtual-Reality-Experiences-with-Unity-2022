using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // The speed at which the object will rotate
    public float rotationSpeed;

    // Flag to determine if the object is currently rotating
    bool isRotating;

    // Update is called once per frame
    void Update()
    {
        // Check if the object is set to rotate
        if (isRotating)
        {
            // Rotate the object around the y-axis based on the rotation speed and the frame rate
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }

    // Toggle the rotation of the object
    public void ToggleRotate()
    {
        // Invert the value of isRotating to toggle the rotation state
        isRotating = !isRotating;
    }

    // Start rotating the object
    public void Rotate()
    {
        // Set isRotating to true to enable rotation
        isRotating = true;
    }

    // Stop rotating the object
    public void NotRotate()
    {
        // Set isRotating to false to disable rotation
        isRotating = false;
    }
}
