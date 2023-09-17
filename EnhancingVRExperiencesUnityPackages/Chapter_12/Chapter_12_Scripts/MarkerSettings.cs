using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class MarkerSettings : MonoBehaviour
{
    // The WhiteboardMarker scripts on the marker objects
    public WhiteboardMarker[] markers;

    // The Slider UI element for pen size
    public Slider sizeSlider;

    // The Image UI element for marker color
    private Image colorImage;

    // The UI panel for the color wheel
    public GameObject colorWheelUI;

    // The Image UI element for the color wheel
    public Image colorWheel;

    // The Image UI element for the color selector
    private Image colorSelector;

    // The Slider UI element for hue
    public Slider hueSlider;

    // The Slider UI element for saturation
    public Slider saturationSlider;

    // The selected color
    private Color selectedColor;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial selected color
        selectedColor = new Color(0.5f, 0.5f, 0.5f);

        // Set the initial color and size for all markers based on the UI elements
        foreach (WhiteboardMarker marker in markers)
        {
            marker.markerColor = colorImage.color;
            marker.penSize = (int)sizeSlider.value;
        }

        // Hide the color wheel UI at startup
        colorWheelUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the selected color based on the hue and saturation values
        float hue = hueSlider.value;
        float saturation = saturationSlider.value;
        selectedColor = Color.HSVToRGB(hue, saturation, 1f);

        // Update the color and size for all markers if the UI elements have changed
        foreach (WhiteboardMarker marker in markers)
        {
            if (marker.markerColor != selectedColor)
            {
                marker.markerColor = selectedColor;
                colorImage.color = selectedColor;
            }
            if (marker.penSize != (int)sizeSlider.value)
            {
                marker.penSize = (int)sizeSlider.value;
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
        selectedColor = GetSelectedColor();
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

    // Calculates the selected color based on the position of the color selector on the color wheel
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

    // Updates the position of the color selector on the color wheel
    public void UpdateColorSelector()
    {
        // Get the position of the color selector in local coordinates
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(colorWheel.rectTransform, Input.mousePosition, null, out localPos);

        // Calculate the hue and saturation values based on the position of the selector
        float hue = Mathf.Atan2(localPos.y, localPos.x) * Mathf.Rad2Deg;
        float saturation = Mathf.Clamp01(localPos.magnitude / (colorWheel.rectTransform.rect.width / 2f));

        // Update the position and color of the color selector
        colorSelector.rectTransform.anchoredPosition = localPos;
        selectedColor = GetSelectedColor();
        colorImage.color = selectedColor;
    }
}
