using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get reference to the map generator, target is the item that this is inspecting.
        MapGenerator mapGen = (MapGenerator)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}
