using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhiteboardMarker : MonoBehaviour
{

    public Transform markerTip;
    public int _penSize = 5;
    private MeshRenderer markerTipMesh;

    private Renderer _renderer;
    public string whiteboardTag;
    public Color markerColor;
    private Color previousColor;

    private Color[] _colors;
    private float _tipHeight;
    private RaycastHit _touch;
    private WhiteboardController _whiteboard;

    private Vector2 _touchPos,_lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;

    // Start is called before the first frame update
    void Start()
    {
        previousColor = markerColor;
        _renderer = markerTip.GetComponent<Renderer>();
        _tipHeight = markerTip.localScale.y;
        
        //markerTipMesh = markerTip.GetComponent<MeshRenderer>();
        //markerTipMesh.material.color = markerColor;
        markerTipMesh = markerTip.GetComponent<MeshRenderer>();
        PickColor();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the materialColor variable has changed
        if (markerColor != previousColor)
        {
            // Update the material with the new color
            PickColor();

            // Update the previousColor variable to the new value of materialColor
            previousColor = markerColor;
        }
        Draw();
    }

    private void PickColor()
    {
        Material markerMaterial = new Material(Shader.Find("Standard"));
        markerMaterial.color = markerColor;
        markerTipMesh.material = markerMaterial;
        SetColors();
        
    }
    private void SetColors()
    {

        _colors = Enumerable.Repeat(_renderer.material.color, _penSize * _penSize).ToArray();
    }
    private void Draw()
    {
        if(Physics.Raycast(markerTip.position,transform.up,out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag(whiteboardTag))
            {
                if(_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<WhiteboardController>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;
                
                    if (_touchedLastFrame)
                    {
                        _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                        for(float f = 0.01f; f < 1.00f; f += 0.01f)
                        {
                            var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                            var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                            _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize,_colors);

                        }

                        transform.rotation = _lastTouchRot;
                        _whiteboard.texture.Apply();
                    }

                    _lastTouchPos = new Vector2(x, y);
                    _lastTouchRot = transform.rotation;
                        _touchedLastFrame = true;
                    return;

                
            }

          

        }


        _whiteboard = null;
        _touchedLastFrame = false;
    }
}
