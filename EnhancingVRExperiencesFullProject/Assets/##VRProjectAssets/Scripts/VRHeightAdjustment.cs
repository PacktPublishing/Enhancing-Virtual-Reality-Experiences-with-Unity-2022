using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class VRHeightAdjustment : MonoBehaviour
{
    public Slider heightSlider;

    public float minHeight = 0.25f;
    public float maxHeight = 2f;

    private CapsuleCollider vrHeight;
    private Transform groundChecker;
    private XROrigin xrOrigin;


    void Start()
    {
        vrHeight = GetComponent<CapsuleCollider>();
        groundChecker = transform.Find("GroundChecker");
        xrOrigin = GetComponent<XROrigin>();

        heightSlider.onValueChanged.AddListener(OnHeightValueChanged);
    }

    void OnHeightValueChanged(float value)
    {
        SetHeight(value);
    }

    void SetHeight(float height)
    {
        // Calculate the scaling factor based on the slider value
        float scaleFactor = height / transform.localScale.y;

        // Scale the capsule collider height and center
        vrHeight.height *= scaleFactor;
        vrHeight.center *= scaleFactor;

        // Move the ground checker
        groundChecker.localPosition = new Vector3(0, -vrHeight.height / 2, 0);

        // Scale the camera height offset
        xrOrigin.CameraYOffset *= scaleFactor;

        // Scale the overall transform of the VR rig
        transform.localScale *= scaleFactor;

        // Clamp the height to the min/max values
        height = Mathf.Clamp(height, minHeight, maxHeight);
    }
}
