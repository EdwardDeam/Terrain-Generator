using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;

    public void DrawTexture(Texture2D texture)
    {
        // Material is only generated at runtime, need to use shared material to view changes in the editor.
        textureRenderer.sharedMaterial.mainTexture = texture;
        // Set the size of the plane to the same as the map.
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
}
