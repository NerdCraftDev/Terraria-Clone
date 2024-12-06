using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Terrain Settings")]
    public int width = 100;
    public int height = 50;
    public int octaves = 4;
    public int dirtLayerHeight = 10;
    public int seed = 0;

    [Tooltip("Controls the extremeness of terrain features. Higher values create more extreme hills, while lower values create gentler slopes.")]
    public float variationIntensity = 10.0f;  // Controls terrain variation without lowering baseline height

    [Header("Tilemap and Tiles")]
    public Tilemap tilemap;                   // Reference to the Tilemap component
    public TileBase dirtTile;                 // Tile for dirt
    public TileBase grassTile;                // Tile for grass
    public TileBase stoneTile;                // Tile for stone

    private int[] heights;

    public GameObject player;

    void Start()
    {
        if (seed == 0) seed = UnityEngine.Random.Range(-1000000, 1000000);
        GenerateTerrain();
        player.transform.position = new Vector3(width/4, (heights[width / 2]+3)/2, -10);
    }

    void GenerateTerrain()
    {
        heights = new int[width];
        // Set a baseline height to center the terrain in the middle of the map
        int baselineHeight = height / 2;

        for (int x = 0; x < width; x++)
        {
            // Calculate Simplex noise, adjust it with variationIntensity, and add to baselineHeight
            float simplexValue = 0;
            float frequency = 0.01f;
            float amplitude = 1;
            float maxValue = 0;  // Used for normalizing result to 0.0 - 1.0

            for (int i = 0; i < octaves; i++)  // Number of octaves
            {
                simplexValue += noise.snoise(new float2((x + seed) * frequency, 0)) * amplitude;
                maxValue += amplitude;

                amplitude *= 0.5f;
                frequency *= 2.0f;
            }

            simplexValue /= maxValue;  // Normalize the result
            int columnHeight = baselineHeight + Mathf.FloorToInt((simplexValue - 0.5f) * variationIntensity);
            heights[x] = columnHeight;

            for (int y = 0; y < height; y++)
            {
                TileBase tileToSet;

                if (y < columnHeight - dirtLayerHeight)
                    tileToSet = stoneTile;    // Stone layer
                else if (y < columnHeight)
                    tileToSet = dirtTile;     // Dirt layer
                else if (y == columnHeight)
                    tileToSet = grassTile;    // Grass layer
                else
                    tileToSet = null;         // Air

                // Set the tile in the Tilemap
                tilemap.SetTile(new Vector3Int(x, y, 0), tileToSet);
            }
        }
    }
}
