using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderController : MonoBehaviour
{
    public string colliderTag;
    public UnityEvent enterEvent;
    public UnityEvent stayEvent;
    public UnityEvent exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == colliderTag)
        {
            enterEvent.Invoke();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == colliderTag)
        {
            stayEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == colliderTag)
        {
            exitEvent.Invoke();
        }
    }
}
