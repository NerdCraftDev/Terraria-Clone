using UnityEngine;
using UnityEngine.Tilemaps;

public class TileInteractor : MonoBehaviour
{
    [Header("Tilemap and Camera")]
    public Tilemap groundTilemap;
    public Camera mainCamera;

    [Header("Mining Settings")]
    public float miningRange = 2f; // How far the player can reach

    [Header("Placement Settings")]
    public TileBase selectedTile; // The tile selected from inventory
    private Inventory inventory;  // Reference to the Inventory component

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MineTile();
        }
        if (Input.GetMouseButton(1))
        {
            PlaceTile();
        }
    }

    void MineTile()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = groundTilemap.WorldToCell(mouseWorldPos);

        // Check if tile is within mining range
        float distance = Vector2.Distance(transform.position, mouseWorldPos);
        if (distance <= miningRange)
        {
            if (groundTilemap.HasTile(tilePos))
            {
                groundTilemap.SetTile(tilePos, null);
            }
        }
    }

    void PlaceTile()
    {
        TileBase selectedTile = inventory.GetSelectedItem(); // Get selected tile from inventory
        if (selectedTile == null) return;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = groundTilemap.WorldToCell(mouseWorldPos);

        // Check if tile is within placing range
        float distance = Vector2.Distance(transform.position, mouseWorldPos);
        if (distance <= miningRange)
        {
            if (groundTilemap.HasTile(tilePos))
            {
                return; // Prevent placing on top of existing tile
                // MineTile();
            }
            groundTilemap.SetTile(tilePos, selectedTile);
        }
    }
}