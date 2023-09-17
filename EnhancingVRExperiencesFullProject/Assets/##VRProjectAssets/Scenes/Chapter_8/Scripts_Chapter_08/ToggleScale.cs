using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleScale : MonoBehaviour
{
    public InputActionAsset inputActionAsset;
    public InputActionReference scaleUp;
    public InputActionReference scaleDown;
    private Vector3 intialscale;
    private bool isScalingUp;
    private bool isScalingDown;

    // Start is called before the first frame update
    void Start()
    {
        intialscale = transform.localScale;
        inputActionAsset.Enable();
        scaleUp.action.performed += ctx => isScalingUp = true;
        scaleUp.action.performed += ctx => isScalingUp = false;
        scaleDown.action.performed += ctx => isScalingDown = true;
        scaleDown.action.performed += ctx => isScalingDown = false;
    }
    private void OnDestroy()
    {
        inputActionAsset.Disable();
    }
    // Update is called once per frame
    private void Update()
    {
        if (isScalingUp)
        {
            transform.localScale += new Vector3(.01f, .01f, .01f);
        }
        else if (isScalingDown)
        {
            transform.localScale -= new Vector3(.01f, .01f, .01f);
        }
    }
    public void ScaleUp()
    {
        isScalingUp = true;
        isScalingDown = false;
    }
    public void ScaleDown()
    {
        isScalingUp = false;
        isScalingDown = true;
    }
    public void ScaleNull()
    {
        isScalingUp = false;
        isScalingDown = false;
    }
}
