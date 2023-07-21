using UnityEngine;


public class WhiteboardController : MonoBehaviour
{

    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);

     void Start()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;

    }


}