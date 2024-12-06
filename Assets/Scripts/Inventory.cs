using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Inventory : MonoBehaviour
{
    public List<TileBase> items = new List<TileBase>();
    public int selectedIndex = 0;

    void Update()
    {
        // Change selected item using number keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) selectedIndex = 3;
        // Add more keys as needed

        // Ensure index is within bounds
        if (selectedIndex >= items.Count)
        {
            selectedIndex = items.Count - 1;
        }
        if (selectedIndex < 0)
        {
            selectedIndex = 0;
        }
    }

    public TileBase GetSelectedItem()
    {
        if (items.Count > 0 && selectedIndex < items.Count)
        {
            return items[selectedIndex];
        }
        return null;
    }
}