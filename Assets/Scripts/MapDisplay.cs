using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;

    public void DrawNosieMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        // It is faster to create a color array and assign all the pixels at once rather than assigning then individually.
        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                // Convert 2D array index to 1D array index
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();

        // Material is only generated at runtime, need to use shared material to view changes in the editor.
        textureRenderer.sharedMaterial.mainTexture = texture;
        // Set the size of the plane to the same as the map.
        textureRenderer.transform.localScale = new Vector3(width, 1, height);
    }
}
