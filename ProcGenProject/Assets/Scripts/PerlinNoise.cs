using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;

    public int depth = 20;

    public float scale = 20;

    private void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);

        //Set Height Map
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    //float array for each height point on the map. 
    float[,] GenerateHeights()
    { 

        float[,] heightPoints = new float [width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //adding perlin noise value to each of the float points on the map.
                heightPoints[x, y] = Noise(x, y);
            }
                
        }

        return heightPoints;
    }


    float Noise (int x, int y)
    {
        //multiply by scale to zoom in and out 
        //converting terrain coordinates into noise map coordinates
        float xPosition = (float) x / width * scale;
        float yPosition = (float) y / height * scale;

        //returning a sample of Perlin Noise Map
        return Mathf.PerlinNoise(xPosition, yPosition);
    }

}
