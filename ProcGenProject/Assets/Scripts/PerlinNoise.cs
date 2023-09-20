using System.Collections;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public int depth = 10;
    public float scale = 20;

    public float offsetX = 100f;
    public float offsetY = 100f;

    Coroutine coroutine;

    private void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        coroutine = StartCoroutine(SetOffset());
    }

    private void FixedUpdate()
    {
        //get/set terrrain data
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    IEnumerator SetOffset()
    {
        while (true)
        {
            offsetX = Random.Range(0f, 9999f);
            offsetY = Random.Range(0f, 9999f);

            yield return new WaitForSeconds(1.5f);
        }
    }


    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);

        //Set Height Map
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    //creating a float array for each height point on the map. 
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
        float xPosition = (float) x / width * scale + offsetX;
        float yPosition = (float) y / height * scale + offsetY;

        //returning a sample of Perlin Noise Map
        return Mathf.PerlinNoise(xPosition, yPosition);
    }

}
