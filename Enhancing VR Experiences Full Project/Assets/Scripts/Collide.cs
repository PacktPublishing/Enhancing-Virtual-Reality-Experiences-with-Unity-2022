using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collide: MonoBehaviour
{
    public string colliderTag;
    public UnityEvent colliderEnterEvent;
    public UnityEvent colliderExitEvent;
    private void OnTriggerEnter(Collider other)
    {

            if (other.tag == colliderTag)
        {
                colliderEnterEvent.Invoke();
               
            }
   
    }

    private void OnTriggerExit(Collider other)
    {

            if (other.tag == colliderTag)
        {
                colliderExitEvent.Invoke();
            
            }
      
    }
}
