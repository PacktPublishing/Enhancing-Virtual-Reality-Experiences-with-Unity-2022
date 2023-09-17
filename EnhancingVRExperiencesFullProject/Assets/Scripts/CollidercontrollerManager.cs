using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
[System.Serializable]
public class ColliderEvents
{
    public List<Collider> colliders;
    public string colliderTag;
    public UnityEvent colliderEnterEvent;
    public UnityEvent colliderExitEvent;
}

public class CollidercontrollerManager : MonoBehaviour
{
    public List<ColliderEvents> colliders;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ColliderEvents colliderEvent in colliders)
        {
            if (colliderEvent.colliders.Contains(collision.collider) && collision.collider.CompareTag(colliderEvent.colliderTag))
            {
                colliderEvent.colliderEnterEvent.Invoke();
                break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        foreach (ColliderEvents colliderEvent in colliders)
        {
            if (colliderEvent.colliders.Contains(collision.collider) && collision.collider.CompareTag(colliderEvent.colliderTag))
            {
                colliderEvent.colliderExitEvent.Invoke();
                break;
            }
        }
    }
}
