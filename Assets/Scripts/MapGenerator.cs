using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private int mapHeight;
    [SerializeField]
    private int mapWidth;

    public bool autoUpdate;

    [Header("Perlin Noise Settings")]
    [SerializeField]
    private int seed;
    [SerializeField]
    private float scale;
    [SerializeField]
    private int octaves;
    [SerializeField]
    [Range(0,1)]
    private float persistance;
    [SerializeField]
    private float lacunarity;
    [SerializeField]
    private Vector2 offset;


    public void GenerateMap ()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, scale, octaves, persistance, lacunarity, offset);

        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNosieMap(noiseMap);
    }

    // Called when one of the variables is changed in the inspector.
    private void OnValidate() {
        if(mapWidth < 1) {
            mapWidth = 1;
        }
        if(mapHeight < 1) {
            mapWidth = 1;
        }
        if(lacunarity < 1) {
            lacunarity = 1;
        }
        if(octaves < 0) {
            octaves = 1;
        }
    }
}
