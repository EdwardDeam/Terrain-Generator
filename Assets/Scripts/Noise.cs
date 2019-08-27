using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    /// <summary>
    /// Generates a 2D Perlin Noise map
    /// </summary>
    /// <param name="mapWidth"></param>
    /// <param name="mapHeight"></param>
    /// <returns>Noise map array</returns>
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale) {
        // Create an emptyy noise map to write to.
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // If the scale is set to zero of less then it will cause errors.
        if(scale <= 0)
        {
            scale = 0.0001f;
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float scaledX = x / scale;
                float scaledY = y / scale;

                float perlinValue = Mathf.PerlinNoise(scaledX, scaledY);
                noiseMap[x, y] = perlinValue;
            }
        }

        return noiseMap;
    }
}
