using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator
{
    /// <summary>
    /// Creates a 2D Texture from a 1D Color array.
    /// </summary>
    /// <param name="colorMap"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns>Colored Texture2D</returns>
    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height) {
        Texture2D texture = new Texture2D(width, height);
        // Remove the bluryness of default Unity filter.
        texture.filterMode = FilterMode.Point;
        // Remove texture wraping
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    /// <summary>
    /// Creates a 2D Texture from a 2D height map.
    /// </summary>
    /// <param name="heightMap"></param>
    /// <returns></returns>
    public static Texture2D TextureFromHeightMap(float[,] heightMap) {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        // It is faster to create a color array and assign all the pixels at once rather than assigning then individually.
        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                // Convert 2D array index to 1D array index
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }
        return TextureFromColorMap(colorMap, width, height);
    }
}
