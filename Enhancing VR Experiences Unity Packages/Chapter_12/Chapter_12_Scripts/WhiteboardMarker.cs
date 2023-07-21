using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{
    // The marker tip transform
    public Transform markerTip;

    // The size of the pen
    public int penSize = 5;

    // The mesh renderer of the marker tip
    private MeshRenderer markerTipMesh;

    // The renderer of the marker tip
    private Renderer _renderer;

    // The tag of the whiteboard object
    public string whiteboardTag;

    // The color of the marker
    public Color markerColor;

    // The previous color of the marker
    private Color previousColor;

    // The array of colors used for drawing
    private Color[] _colors;

    // The height of the marker tip
    private float _tipHeight;

    // The RaycastHit information for detecting touches on the whiteboard
    private RaycastHit _touch;

    // Reference to the WhiteboardController script
    private WhiteboardController _whiteboard;

    // The position of the current touch
    private Vector2 _touchPos;

    // The position of the last touch
    private Vector2 _lastTouchPos;

    // Flag indicating if the marker was touched in the previous frame
    private bool _touchedLastFrame;

    // The rotation of the last touch
    private Quaternion _lastTouchRot;

    // Start is called before the first frame update
    void Start()
    {
        // Store the initial color of the marker
        previousColor = markerColor;

        // Get the renderer component of the marker tip
        _renderer = markerTip.GetComponent<Renderer>();

        // Get the height of the marker tip
        _tipHeight = markerTip.localScale.y;

        // Get the mesh renderer of the marker tip
        markerTipMesh = markerTip.GetComponent<MeshRenderer>();

        // Set the color of the marker tip
        PickColor();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the color of the marker has changed
        if (markerColor != previousColor)
        {
            // Update the color of the marker tip
            PickColor();

            // Update the previous color variable
            previousColor = markerColor;
        }

        // Draw on the whiteboard
        Draw();
    }

    // Set the color of the marker tip
    private void PickColor()
    {
        // Create a new material with the specified color
        Material markerMaterial = new Material(Shader.Find("Standard"));
        markerMaterial.color = markerColor;

        // Set the material of the marker tip
        markerTipMesh.material = markerMaterial;

        // Set the colors used for drawing
        SetColors();
    }

    // Set the colors used for drawing
    private void SetColors()
    {
        _colors = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
    }

    // Draw on the whiteboard
    private void Draw()
    {
        if (Physics.Raycast(markerTip.position, transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag(whiteboardTag))
            {
                if (_whiteboard == null)
                {
                    // Get the WhiteboardController script from the whiteboard object
                    _whiteboard = _touch.transform.GetComponent<WhiteboardController>();
                }

                // Get the texture coordinates of the touch position
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                // Calculate the position in the texture based on the pen size and touch position
                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (penSize / 2));

                // Return if the position is outside the texture bounds
                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x)
                    return;

                if (_touchedLastFrame)
                {
                    // Set the pixels in the texture for the pen size at the current position
                    _whiteboard.texture.SetPixels(x, y, penSize, penSize, _colors);

                    // Smoothly interpolate between the last touch position and the current position
                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, penSize, penSize, _colors);
                    }

                    // Restore the last touch rotation
                    transform.rotation = _lastTouchRot;

                    // Apply the changes to the texture
                    _whiteboard.texture.Apply();
                }

                // Store the current touch position and rotation for interpolation
                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;

                // Update the flag for touchedLastFrame
                _touchedLastFrame = true;

                return;
            }
        }

        // Reset the whiteboard reference and the touchedLastFrame flag
        _whiteboard = null;
        _touchedLastFrame = false;
    }
}
