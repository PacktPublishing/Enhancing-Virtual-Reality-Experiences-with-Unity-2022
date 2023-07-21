using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortalDestinationSettings : MonoBehaviour
{
    public PortalManager currentPortal;
    public PortalManager[] portals;
    public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        portals = FindObjectsOfType<PortalManager>();
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        for (int i = 0; i < portals.Length; i++)
        {
            dropdown.options[i].text = portals[i].destinationName;
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTypeFromIndex(int index)
    {

            currentPortal.destination = portals[index].portalAnchor.gameObject;
            dropdown.options[index].text = portals[index].destinationName;


    }
}
