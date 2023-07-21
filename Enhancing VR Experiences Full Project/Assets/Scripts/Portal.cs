using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Portal : MonoBehaviour
{
    public string playerTag;
    public GameObject destination;
    
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        // check if the tag of the collider is the same as the playerTag variable
        if (other.gameObject.tag == playerTag)
        {
            // move the player to the destination's position
            other.transform.position = destination.transform.position;
        }
    }
}
