using UnityEngine;

public static class MeshGenerator
{
    /// <summary>
    /// Generates a Mesh data from a height map.
    /// </summary>
    /// <param name="heightMap"> noise map used for the terrain Y axis</param>
    /// <returns>MeshData</returns>
    public static MeshData GenerateTerrainMeshData(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        // Used to set the mesh in the center of the world (0,0)
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        // Keeps track of what vertex we are at.
        int vertexIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);
                // Ignore the Right and left vertices of the map
                if(x < width-1 && y < height - 1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }
                vertexIndex++;
            }
        }
        return meshData;
    }
}

/// <summary>
/// Class that holds data for and an create a Mesh, Used with MeshGenerator.
/// </summary>
public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    // Keeps track of what triangle we are at.
    int triangleIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        triangles = new int[(meshHeight - 1) * (meshWidth - 1) * 6];
        uvs = new Vector2[meshWidth * meshHeight];
    }

    /// <summary>
    /// Helper Method to add Triangles.
    /// </summary>
    /// <param name="a">Vertex A</param>
    /// <param name="b">Vertex B</param>
    /// <param name="c">Vertex C</param>
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }
    /// <summary>
    /// This should only be called once the mesh data has been populated.
    /// </summary>
    /// <returns></returns>
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles,
            uv = uvs
        };
        mesh.RecalculateNormals();
        return mesh;
    }
}
