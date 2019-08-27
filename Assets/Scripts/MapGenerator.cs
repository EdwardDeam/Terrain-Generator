using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private int mapHeight;
    [SerializeField]
    private int mapWidth;
    // Noise map scale, determines at what distance to view the noise map.
    public float scale;

    public void GenerateMap ()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, scale);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNosieMap(noiseMap);
    }
}
