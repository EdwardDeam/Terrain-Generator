using System;
using UnityEngine;

public static class Noise
{
    /// <summary>
    /// Generates a 2D Perlin Noise map
    /// </summary>
    /// <param name="mapWidth">Array width</param>
    /// <param name="mapHeight">Array height</param>
    /// <param name="seed">Random seed to sample from</param>
    /// <param name="scale">What distance to view the noise map.</param>
    /// <param name="octaves">Number of Passes</param>
    /// <param name="persistency">How much to decrease amplitude per octave</param>
    /// <param name="lacunarity">How much to increase frequency per octave</param>
    /// <param name="offset">Custom offset for scrolling the map</param>
    /// <returns>Noise map array</returns>
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {
        // Create an emptyy noise map to write to.
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // Psudo Random Number Generator
        System.Random prng = new System.Random(seed);

        // To get more interesting noise sample each octave from a different location.
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            // If the Perlin function get a number too high it will return similar values for each point.
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        // If the scale is set to zero of less then it will cause errors.
        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        // Keep track of high low values for normalizing array at the end.
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        // Find half map length values to let scale zoom to a central point.
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // Variables effect how much noise value should change.
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    // The higher the frequency the more rapidly the noise values will change
                    float scaledX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float scaledY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    // * 2 + 1 so that PerlinNoise() will return a number between -1 and 1 for more interesting terrain.
                    float perlinValue = Mathf.PerlinNoise(scaledX, scaledY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    // Decrease the amplitude effect for the next octave.
                    amplitude *= persistence;
                    // Increase frequency to 
                    frequency *= lacunarity;
                }
                // Save the Highest and Lowest values in the array.
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                // Save the generated noise height.
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                // Normalise noise map to a value between-1 & 1;
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }
}
