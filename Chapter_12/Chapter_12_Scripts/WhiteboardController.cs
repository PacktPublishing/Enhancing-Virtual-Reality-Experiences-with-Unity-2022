using UnityEngine;

public class WhiteboardController : MonoBehaviour
{
    // The texture to be displayed on the whiteboard
    public Texture2D texture;

    // The size of the texture
    public Vector2 textureSize = new Vector2(2048, 2048);

    void Start()
    {
        // Get the Renderer component of the object
        var r = GetComponent<Renderer>();

        // Create a new Texture2D with the specified size
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);

        // Set the mainTexture of the material to the created texture
        r.material.mainTexture = texture;
    }
}
