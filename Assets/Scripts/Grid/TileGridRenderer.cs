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
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class TileGridRenderer : MonoBehaviour
//{
//    [SerializeField] private int gridWidth = 20;
//    [SerializeField] private int gridHeight = 20;
//    [SerializeField] private float tileWidth = 1f;
//    [SerializeField] private float tileHeight = 0.5f;

//    private TileGrid grid;
//    private Sprite tileSprite;

//    void Start()
//    {
//        tileSprite = CreateDiamondSprite();
//        grid = new TileGrid(gridWidth, gridHeight);
//        RenderGrid();
//    }

//    void Update()
//    {
//        if (Mouse.current.leftButton.wasPressedThisFrame)
//            HandleTileClick();
//    }

//    void RenderGrid()
//    {
//        for (int x = 0; x < grid.width; x++)
//        {
//            for (int y = 0; y < grid.height; y++)
//            {
//                Vector3 worldPos = GridToWorld(x, y);

//                GameObject tile = new GameObject("Tile_" + x + "_" + y);
//                tile.transform.position = worldPos;
//                tile.transform.SetParent(transform);

//                SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
//                sr.sprite = tileSprite;
//                sr.color = new Color(0.55f, 0.65f, 0.45f, 1f);
//                sr.sortingOrder = x + y;
//            }
//        }
//    }

//    Vector3 GridToWorld(int x, int y)
//    {
//        float wx = (x - y) * tileWidth * 0.5f;
//        float wy = (x + y) * tileHeight * 0.5f;
//        return new Vector3(wx, wy, 0);
//    }

//    void WorldToGrid(Vector3 worldPos, out int gridX, out int gridY)
//    {
//        float fx = (worldPos.x / (tileWidth * 0.5f) + worldPos.y / (tileHeight * 0.5f)) * 0.5f;
//        float fy = (worldPos.y / (tileHeight * 0.5f) - worldPos.x / (tileWidth * 0.5f)) * 0.5f;
//        gridX = Mathf.RoundToInt(fx);
//        gridY = Mathf.RoundToInt(fy);
//    }

//    void HandleTileClick()
//    {
//        Vector2 mousePos = Mouse.current.position.ReadValue();
//        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
//        worldPos.z = 0;

//        WorldToGrid(worldPos, out int x, out int y);

//        TileData tile = grid.GetTile(x, y);
//        if (tile != null)
//            Debug.Log("Clicked tile: (" + x + ", " + y + ") Type: " + tile.tileType);
//    }

//    Sprite CreateDiamondSprite()
//    {
//        int texWidth = 128;
//        int texHeight = 64;
//        Texture2D tex = new Texture2D(texWidth, texHeight, TextureFormat.RGBA32, false);
//        tex.filterMode = FilterMode.Point;

//        for (int py = 0; py < texHeight; py++)
//        {
//            for (int px = 0; px < texWidth; px++)
//            {
//                float nx = (px / (float)texWidth) - 0.5f;
//                float ny = (py / (float)texHeight) - 0.5f;
//                bool inside = (Mathf.Abs(nx) + Mathf.Abs(ny)) < 0.49f;
//                tex.SetPixel(px, py, inside ? Color.white : Color.clear);
//            }
//        }

//        tex.Apply();
//        return Sprite.Create(tex, new Rect(0, 0, texWidth, texHeight), new Vector2(0.5f, 0.5f), texWidth);
//    }
//}
#endregion
#region Phase 1 Sprint 3 - Tile Grid Rendering with State
//using UnityEngine;

//public class TileGridRenderer : MonoBehaviour
//{
//    [SerializeField] private int gridWidth = 20;
//    [SerializeField] private int gridHeight = 20;
//    [SerializeField] private float tileWidth = 1f;
//    [SerializeField] private float tileHeight = 0.5f;

//    public TileGrid Grid { get; private set; }

//    private Sprite tileSprite;
//    private SpriteRenderer[,] tileRenderers;

//    private static readonly Color colorEmpty = new Color(0.55f, 0.65f, 0.45f, 1f);
//    private static readonly Color colorOccupied = new Color(0.75f, 0.65f, 0.45f, 1f);
//    private static readonly Color colorNPCCamp = new Color(0.75f, 0.35f, 0.35f, 1f);
//    private static readonly Color colorResource = new Color(0.35f, 0.55f, 0.30f, 1f);

//    void Start()
//    {
//        tileSprite = CreateDiamondSprite();
//        Grid = new TileGrid(gridWidth, gridHeight);
//        tileRenderers = new SpriteRenderer[gridWidth, gridHeight];
//        RenderGrid();
//    }

//    void RenderGrid()
//    {
//        for (int x = 0; x < Grid.width; x++)
//        {
//            for (int y = 0; y < Grid.height; y++)
//            {
//                Vector3 worldPos = GridToWorld(x, y);

//                GameObject tile = new GameObject("Tile_" + x + "_" + y);
//                tile.transform.position = worldPos;
//                tile.transform.SetParent(transform);

//                SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
//                sr.sprite = tileSprite;
//                sr.color = colorEmpty;
//                sr.sortingOrder = x + y;

//                tileRenderers[x, y] = sr;
//            }
//        }
//    }

//    public void RefreshTile(int x, int y)
//    {
//        TileData tile = Grid.GetTile(x, y);
//        if (tile == null) return;

//        SpriteRenderer sr = tileRenderers[x, y];
//        if (sr == null) return;

//        switch (tile.tileType)
//        {
//            case TileType.Empty: sr.color = colorEmpty; break;
//            case TileType.Occupied: sr.color = colorOccupied; break;
//            case TileType.NPCCamp: sr.color = colorNPCCamp; break;
//            case TileType.Resource: sr.color = colorResource; break;
//        }
//    }

//    public Vector3 GridToWorld(int x, int y)
//    {
//        float wx = (x - y) * tileWidth * 0.5f;
//        float wy = (x + y) * tileHeight * 0.5f;
//        return new Vector3(wx, wy, 0);
//    }

//    public void GetGridCoordinates(Vector3 worldPos, out int gridX, out int gridY)
//    {
//        float fx = (worldPos.x / (tileWidth * 0.5f) + worldPos.y / (tileHeight * 0.5f)) * 0.5f;
//        float fy = (worldPos.y / (tileHeight * 0.5f) - worldPos.x / (tileWidth * 0.5f)) * 0.5f;
//        gridX = Mathf.RoundToInt(fx);
//        gridY = Mathf.RoundToInt(fy);
//    }

//    Sprite CreateDiamondSprite()
//    {
//        int texWidth = 128;
//        int texHeight = 64;
//        Texture2D tex = new Texture2D(texWidth, texHeight, TextureFormat.RGBA32, false);
//        tex.filterMode = FilterMode.Point;

//        for (int py = 0; py < texHeight; py++)
//        {
//            for (int px = 0; px < texWidth; px++)
//            {
//                float nx = (px / (float)texWidth) - 0.5f;
//                float ny = (py / (float)texHeight) - 0.5f;
//                bool inside = (Mathf.Abs(nx) + Mathf.Abs(ny)) < 0.49f;
//                tex.SetPixel(px, py, inside ? Color.white : Color.clear);
//            }
//        }

//        tex.Apply();
//        return Sprite.Create(tex, new Rect(0, 0, texWidth, texHeight), new Vector2(0.5f, 0.5f), texWidth);
//    }
//}
#endregion
#region Phase 1 Sprint 6 - Tile Grid Rendering with Tilemap Alignment
using UnityEngine;

public class TileGridRenderer : MonoBehaviour
{
    [Header("Grid Size")]
    [Tooltip("Number of tiles across X axis.")]
    [SerializeField] private int gridWidth = 20;
    [Tooltip("Number of tiles across Y axis.")]
    [SerializeField] private int gridHeight = 20;

    [Header("Tile Dimensions")]
    [Tooltip("Width of one tile in Unity units. Must match Grid Cell Size X on the Tilemap Grid object.")]
    [SerializeField] private float tileWidth = 1f;
    [Tooltip("Height of one tile in Unity units. Must match Grid Cell Size Y on the Tilemap Grid object.")]
    [SerializeField] private float tileHeight = 0.5f;

    [Header("Tilemap Alignment")]
    [Tooltip("Nudge the logic grid to align with the tilemap. Adjust Y until diamonds sit flush on top of tilemap tiles.")]
    [SerializeField] private Vector2 gridOffset = Vector2.zero;

    [Header("Diamond Appearance")]
    [Tooltip("Opacity of the diamond overlay. 0 = invisible, 1 = fully opaque. 0.4 is recommended.")]
    [Range(0f, 1f)]
    [SerializeField] private float gridAlpha = 0.4f;

    [Header("Tile State Colors")]
    [Tooltip("Shown on empty buildable tiles.")]
    [SerializeField] private Color colorEmpty = new Color(0.55f, 0.65f, 0.45f, 1f);
    [Tooltip("Shown when a building occupies the tile.")]
    [SerializeField] private Color colorOccupied = new Color(0.75f, 0.65f, 0.45f, 1f);
    [Tooltip("Shown on NPC camp tiles.")]
    [SerializeField] private Color colorNPCCamp = new Color(0.75f, 0.35f, 0.35f, 1f);
    [Tooltip("Shown on resource tiles.")]
    [SerializeField] private Color colorResource = new Color(0.35f, 0.55f, 0.30f, 1f);

    [Header("Debug")]
    [Tooltip("Untick to hide all diamonds and see only the tilemap.")]
    [SerializeField] private bool showGrid = true;

    public TileGrid Grid { get; private set; }

    private Sprite tileSprite;
    private SpriteRenderer[,] tileRenderers;

    void Start()
    {
        tileSprite = CreateDiamondSprite();
        Grid = new TileGrid(gridWidth, gridHeight);
        tileRenderers = new SpriteRenderer[gridWidth, gridHeight];
        RenderGrid();
    }

    void RenderGrid()
    {
        for (int x = 0; x < Grid.width; x++)
        {
            for (int y = 0; y < Grid.height; y++)
            {
                Vector3 worldPos = GridToWorld(x, y);

                GameObject tile = new GameObject("Tile_" + x + "_" + y);
                tile.transform.position = worldPos;
                tile.transform.SetParent(transform);

                SpriteRenderer sr = tile.AddComponent<SpriteRenderer>();
                sr.sprite = tileSprite;
                sr.color = WithAlpha(colorEmpty);
                sr.sortingLayerName = "LogicGrid";
                sr.sortingOrder = x + y;

                tileRenderers[x, y] = sr;
            }
        }
    }

    public void RefreshTile(int x, int y)
    {
        TileData tile = Grid.GetTile(x, y);
        if (tile == null) return;

        SpriteRenderer sr = tileRenderers[x, y];
        if (sr == null) return;

        switch (tile.tileType)
        {
            case TileType.Empty: sr.color = WithAlpha(colorEmpty); break;
            case TileType.Occupied: sr.color = WithAlpha(colorOccupied); break;
            case TileType.NPCCamp: sr.color = WithAlpha(colorNPCCamp); break;
            case TileType.Resource: sr.color = WithAlpha(colorResource); break;
        }
    }

    public Vector3 GridToWorld(int x, int y)
    {
        float wx = (x - y) * tileWidth * 0.5f + gridOffset.x;
        float wy = (x + y) * tileHeight * 0.5f + gridOffset.y;
        return new Vector3(wx, wy, 0);
    }

    public void GetGridCoordinates(Vector3 worldPos, out int gridX, out int gridY)
    {
        float ax = worldPos.x - gridOffset.x;
        float ay = worldPos.y - gridOffset.y;
        float fx = (ax / (tileWidth * 0.5f) + ay / (tileHeight * 0.5f)) * 0.5f;
        float fy = (ay / (tileHeight * 0.5f) - ax / (tileWidth * 0.5f)) * 0.5f;
        gridX = Mathf.RoundToInt(fx);
        gridY = Mathf.RoundToInt(fy);
    }

    void Update()
    {
        if (tileRenderers == null) return;

        foreach (SpriteRenderer sr in tileRenderers)
        {
            if (sr != null)
                sr.enabled = showGrid;
        }
    }

    Color WithAlpha(Color c)
    {
        return new Color(c.r, c.g, c.b, gridAlpha);
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