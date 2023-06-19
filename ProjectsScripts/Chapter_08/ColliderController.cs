using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderController : MonoBehaviour
{
    // The tag of the colliders that will trigger the events
    public string colliderTag;

    // UnityEvent invoked when an object enters the collider
    public UnityEvent enterEvent;

    // UnityEvent invoked while an object stays within the collider
    public UnityEvent stayEvent;

    // UnityEvent invoked when an object exits the collider
    public UnityEvent exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider's tag matches the specified colliderTag
        if (other.tag == colliderTag)
        {
            // Invoke the enterEvent when the collider is entered by an object with the matching tag
            enterEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the collider's tag matches the specified colliderTag
        if (other.tag == colliderTag)
        {
            // Invoke the stayEvent while an object with the matching tag stays within the collider
            stayEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider's tag matches the specified colliderTag
        if (other.tag == colliderTag)
        {
            // Invoke the exitEvent when an object with the matching tag exits the collider
            exitEvent.Invoke();
        }
    }
}
