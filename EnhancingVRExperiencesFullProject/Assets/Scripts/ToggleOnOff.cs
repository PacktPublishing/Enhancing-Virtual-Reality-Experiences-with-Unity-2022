using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Toggle()
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
    }
}
