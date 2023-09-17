using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    public string playerTag;
    public string destinationName;
    public GameObject destination;
    public GameObject player;
    public float portalCooldown;
    private float cooldownTime;
    private bool onCooldown;

    public Transform portalAnchor;//

    private void Start()
    {
        cooldownTime = 0.0f;
        onCooldown = false;
        player = GameObject.FindWithTag("Player");//
        portalAnchor = GetComponentInChildren<Transform>().Find("PortalAnchor1");
    }
    private void Update()
    {
        if (onCooldown)
        {
            cooldownTime -= Time.deltaTime;
            if (cooldownTime <= 0.0f)
            {
                onCooldown = false;
            }
        }
    }
    public void StartCoolDown()
    {
        if (!onCooldown)
        {
            cooldownTime = portalCooldown;
            onCooldown = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.tag == playerTag&&!onCooldown)
        {
            StartCoolDown();
            player.transform.position = destination.transform.position;
            
        }
    }


}
