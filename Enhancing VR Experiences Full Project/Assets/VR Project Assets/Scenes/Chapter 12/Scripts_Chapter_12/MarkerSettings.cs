using UnityEngine;
using UnityEngine.UI;

public class MarkerSettings : MonoBehaviour
{
    public WhiteboardMarker[] markers; // The WhiteboardMarker script on the marker object
    public Slider sizeSlider; // The Slider UI element for pen size
    private Image colorImage; // The Image UI element for marker color
    public GameObject colorWheelUI;
    public Image colorWheel;
    private Image colorSelector;
    public Slider hueSlider;
    public Slider saturationSlider;
    private float previousSaturationValue;

    private float previousHueValue;

    private Color _selectedColor;

    // Start is called before the first frame update
    void Start()
    {
        _selectedColor = new Color(0.5f, 0.5f, 0.5f);
        // Set the initial color and size to the values of the UI elements
       foreach(WhiteboardMarker marker in markers)
        {
            marker.markerColor = colorImage.color;
            marker._penSize = (int)sizeSlider.value;
        }
            

        // Hide the color wheel UI at startup
        //colorWheelUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float hue = hueSlider.value;
        float saturation = saturationSlider.value;

        _selectedColor = Color.HSVToRGB(hue, saturation, 1f);
        colorWheel.color = _selectedColor;
        // Update the color and size if the UI elements have changed
        foreach (WhiteboardMarker marker in markers)
        {
            if (marker.markerColor != _selectedColor)
            {
                marker.markerColor = _selectedColor;
                colorImage.color = _selectedColor;
            }
            if (marker._penSize != (int)sizeSlider.value)
            {
                marker._penSize = (int)sizeSlider.value;
            }
            if (hueSlider.value != previousHueValue || saturationSlider.value != previousSaturationValue)
            {
                GetSelectedColor();
            }
        }
    }

    // Toggles the visibility of the UI panel
    public void ToggleSettingsPanel(bool show)
    {
        gameObject.SetActive(show);
    }

    // Called when the user confirms their color choice
    public void SelectColor()
    {
        _selectedColor = GetSelectedColor();
        colorWheelUI.SetActive(false);
    }

    // Called when the user cancels their color choice
    public void CancelColor()
    {
        colorWheelUI.SetActive(false);
    }

    // Shows the color wheel UI
    public void ShowColorWheel()
    {
        colorWheelUI.SetActive(true);
    }

    private Color GetSelectedColor()
    {
        // Get the hue and saturation values from the sliders
        float hue = hueSlider.value * 360f;
        float saturation = saturationSlider.value;

        // Calculate the RGB values from the hue and saturation
        float chroma = saturation;
        float huePrime = hue / 60f;
        float x = chroma * (1f - Mathf.Abs(huePrime % 2f - 1f));
        float r1 = 0, g1 = 0, b1 = 0;
        if (huePrime < 1f)
        {
            r1 = chroma;
            g1 = x;
        }
        else if (huePrime < 2f)
        {
            r1 = x;
            g1 = chroma;
        }
        else if (huePrime < 3f)
        {
            g1 = chroma;
            b1 = x;
        }
        else if (huePrime < 4f)
        {
            g1 = x;
            b1 = chroma;
        }
        else if (huePrime < 5f)
        {
            r1 = x;
            b1 = chroma;
        }
        else
        {
            r1 = chroma;
            b1 = x;
        }

        // Add the brightness value to each RGB value
        float brightness = 1f;
        float m = brightness - chroma;
        float r = r1 + m;
        float g = g1 + m;
        float b = b1 + m;

        // Return the calculated color
        return new Color(r, g, b);
    }

    // Update the position of the color selector on the color wheel
    public void UpdateColorSelector()
    {
        // Get the position of the color selector on the color wheel
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(colorWheel.rectTransform, Input.mousePosition, null, out localPos);

        // Calculate the hue and saturation values based on the position of the selector
        float hue = Mathf.Atan2(localPos.y, localPos.x) * Mathf.Rad2Deg;
        float saturation = Mathf.Clamp01(localPos.magnitude / (colorWheel.rectTransform.rect.width / 2f));

        // Update the position and color of the color selector
        colorSelector.rectTransform.anchoredPosition = localPos;
        _selectedColor = GetSelectedColor();
        colorImage.color = _selectedColor;
    }









    //public WhiteboardMarker marker; // The WhiteboardMarker script on the marker object
    //public Slider sizeSlider; // The Slider UI element for pen size
    //public Image colorImage; // The Image UI element for marker color
    //public Image colorWheel;
    //public Image colorSelector;
    //public Slider hueSlider;
    //public Slider saturationSlider;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    // Set the initial color and size to the values of the UI elements
    //    marker.markerColor = colorImage.color;
    //    marker._penSize = (int)sizeSlider.value;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // Update the color and size if the UI elements have changed
    //    if (marker.markerColor != colorImage.color)
    //    {
    //        marker.markerColor = colorImage.color;
    //    }
    //    if (marker._penSize != (int)sizeSlider.value)
    //    {
    //        marker._penSize = (int)sizeSlider.value;
    //    }
    //}

    //// Toggles the visibility of the UI panel
    //public void ToggleSettingsPanel(bool show)
    //{
    //    gameObject.SetActive(show);
    //}

    //// Called when the user confirms their color choice
    //public void SelectColor()
    //{
    //    Color selectedColor = GetSelectedColor();
    //    // Do something with the selected color, such as updating a material
    //    // or setting the color of a game object
    //}
    //private Color GetSelectedColor()
    //{
    //    // Get the hue and saturation values from the sliders
    //    float hue = hueSlider.value * 360f;
    //    float saturation = saturationSlider.value;

    //    // Calculate the RGB values from the hue and saturation
    //    float chroma = saturation;
    //    float huePrime = hue / 60f;
    //    float x = chroma * (1f - Mathf.Abs(huePrime % 2f - 1f));
    //    float r1 = 0, g1 = 0, b1 = 0;
    //    if (huePrime < 1f)
    //    {
    //        r1 = chroma;
    //        g = x;
    //    }
    //    else if (huePrime < 2f)
    //    {
    //        r1 = x;
    //        g1 = chroma;
    //    }
    //    else if (huePrime < 3f)
    //    {
    //        g1 = chroma;
    //        b1 = x;
    //    }
    //    else if (huePrime < 4f)
    //    {
    //        g1 = x;
    //        b1 = chroma;
    //    }
    //    else if (huePrime < 5f)
    //    {
    //        r1 = x;
    //        b1 = chroma;
    //    }
    //    else
    //    {
    //        r1 = chroma;
    //        b1 = x;
    //    }

    //    // Add the brightness value to each RGB value
    //    float brightness = 1f;
    //    float m = brightness - chroma;
    //    float r = r1 + m;
    //    float g = g1 + m;
    //    float b = b1 + m;

    //    // Return the calculated color
    //    return new Color(r, g, b);
    //}
}
