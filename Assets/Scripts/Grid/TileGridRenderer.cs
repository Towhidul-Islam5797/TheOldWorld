#region Summary
/// <summary>
/// Renders a tile grid in an isometric view using Unity's SpriteRenderer. 
/// Each tile is represented as a diamond-shaped sprite, and the grid is generated based on specified width and height parameters. 
/// The script also handles mouse input to detect clicks on individual tiles, allowing for interaction with the tile data. 
/// The tile grid is created using a custom TileGrid class that manages the underlying data structure of the tiles. 
/// This script should be attached to an empty GameObject in the Unity scene to visualize the tile grid.
/// Note: The tile sprite is generated procedurally as a simple diamond shape, but it can be replaced with a custom sprite if desired.
#endregion
#region Phase 1 Sprint 2 - Tile Grid Rendering
using UnityEngine;
using UnityEngine.InputSystem;

public class TileGridRenderer : MonoBehaviour
{
    [SerializeField] private int gridWidth = 20;
    [SerializeField] private int gridHeight = 20;
    [SerializeField] private float tileWidth = 1f;
    [SerializeField] private float tileHeight = 0.5f;

    private TileGrid grid;
    private Sprite tileSprite;

    void Start()
    {
        tileSprite = CreateDiamondSprite();
        grid = new TileGrid(gridWidth, gridHeight);
        RenderGrid();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            HandleTileClick();
    }

    void RenderGrid()
    {
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                Vector3 worldPos = GridToWorld(x, y);

                GameObject tile = new GameObject("Tile_" + x + "_" + y);
                tile.transform.position = worldPos;
                tile.transform.SetParent(transform);

                SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
                sr.sprite = tileSprite;
                sr.color = new Color(0.55f, 0.65f, 0.45f, 1f);
                sr.sortingOrder = x + y;
            }
        }
    }

    Vector3 GridToWorld(int x, int y)
    {
        float wx = (x - y) * tileWidth * 0.5f;
        float wy = (x + y) * tileHeight * 0.5f;
        return new Vector3(wx, wy, 0);
    }

    void WorldToGrid(Vector3 worldPos, out int gridX, out int gridY)
    {
        float fx = (worldPos.x / (tileWidth * 0.5f) + worldPos.y / (tileHeight * 0.5f)) * 0.5f;
        float fy = (worldPos.y / (tileHeight * 0.5f) - worldPos.x / (tileWidth * 0.5f)) * 0.5f;
        gridX = Mathf.RoundToInt(fx);
        gridY = Mathf.RoundToInt(fy);
    }

    void HandleTileClick()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        worldPos.z = 0;

        WorldToGrid(worldPos, out int x, out int y);

        TileData tile = grid.GetTile(x, y);
        if (tile != null)
            Debug.Log("Clicked tile: (" + x + ", " + y + ") Type: " + tile.tileType);
    }

    Sprite CreateDiamondSprite()
    {
        int texWidth = 128;
        int texHeight = 64;
        Texture2D tex = new Texture2D(texWidth, texHeight, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Point;

        for (int py = 0; py < texHeight; py++)
        {
            for (int px = 0; px < texWidth; px++)
            {
                float nx = (px / (float)texWidth) - 0.5f;
                float ny = (py / (float)texHeight) - 0.5f;
                bool inside = (Mathf.Abs(nx) + Mathf.Abs(ny)) < 0.49f;
                tex.SetPixel(px, py, inside ? Color.white : Color.clear);
            }
        }

        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, texWidth, texHeight), new Vector2(0.5f, 0.5f), texWidth);
    }
}
#endregion