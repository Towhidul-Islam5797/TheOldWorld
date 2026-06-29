#region Summary
/// <summary>
/// BuildingPlacer is a component responsible for handling player interactions related to placing and upgrading buildings on the game grid.
/// It listens for specific input (e.g., pressing the 'B' key to toggle place mode) and mouse clicks to determine where the player wants to place or upgrade a building.
/// The BuildingPlacer interacts with the TileGridRenderer to convert screen coordinates to grid coordinates and with the BuildingManager to execute building placements and upgrades.
/// It also provides feedback in the console about the current mode (placing or selecting) and details about selected buildings.
/// Key functions of the BuildingPlacer include:
/// 1. Toggling between place mode and select mode based on player input.
/// 2. Handling mouse clicks to either place a new building (if in place mode) or select/upgrade an existing building (if in select mode).
/// 3. Providing a method to set the currently selected building configuration for placement.
/// Example usage:
/// - When the player presses the 'B' key, the BuildingPlacer toggles place mode, allowing the player to click on the grid to place a building.
/// - When the player clicks on an occupied tile while not in place mode, the BuildingPlacer retrieves the building information and attempts to upgrade it through the BuildingManager.
/// Note: The BuildingPlacer relies on the BuildingManager, TileGridRenderer, and CameraController to function properly, and it assumes that these classes are implemented with the 
///         necessary properties and methods to support building placement and selection operations.
#endregion
#region Phase 1 Sprint 3 - Building Placer Implementation
//using UnityEngine;

//public class BuildingPlacer : MonoBehaviour
//{
//    [SerializeField] private BuildingConfig selectedConfig;

//    private bool placeMode;
//    private TileGridRenderer gridRenderer;
//    private CameraController cameraController;

//    void Start()
//    {
//        gridRenderer = FindFirstObjectByType<TileGridRenderer>();
//        cameraController = FindFirstObjectByType<CameraController>();
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            placeMode = !placeMode;
//            Debug.Log("Place mode: " + placeMode);
//        }

//        if (Input.GetKeyDown(KeyCode.Escape))
//            placeMode = false;

//        if (Input.GetMouseButtonUp(0) && !cameraController.IsDragging)
//            HandleClick();
//    }

//    void HandleClick()
//    {
//        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        worldPos.z = 0;

//        gridRenderer.GetGridCoordinates(worldPos, out int x, out int y);

//        if (placeMode)
//        {
//            if (selectedConfig != null)
//                BuildingManager.Instance.PlaceBuilding(selectedConfig, x, y);
//        }
//        else
//        {
//            TileData tile = gridRenderer.Grid.GetTile(x, y);
//            if (tile == null) return;

//            if (tile.occupant != null)
//            {
//                BuildingState b = tile.occupant;
//                Debug.Log("Selected: " + b.config.buildingName + " Level " + b.level + (b.isUpgrading ? " (upgrading...)" : ""));
//                BuildingManager.Instance.UpgradeBuilding(x, y);
//            }
//            else
//            {
//                Debug.Log("Empty tile: (" + x + ", " + y + ")");
//            }
//        }
//    }

//    public void SetBuilding(BuildingConfig config)
//    {
//        selectedConfig = config;
//        placeMode = true;
//    }
//}
#endregion
#region Phase 1 Sprint 9 - Building Placer with Footprint Preview
//using UnityEngine;

//public class BuildingPlacer : MonoBehaviour
//{
//    [SerializeField] private BuildingConfig selectedConfig;

//    private static readonly Color colorValid = new Color(0.2f, 0.9f, 0.2f, 0.6f);
//    private static readonly Color colorInvalid = new Color(0.9f, 0.2f, 0.2f, 0.6f);

//    private bool placeMode;
//    private TileGridRenderer gridRenderer;
//    private CameraController cameraController;

//    // Tracks which tiles are currently highlighted so we can restore them
//    private int previewOriginX = -1;
//    private int previewOriginY = -1;

//    void Start()
//    {
//        gridRenderer = FindFirstObjectByType<TileGridRenderer>();
//        cameraController = FindFirstObjectByType<CameraController>();
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            placeMode = !placeMode;
//            if (!placeMode) ClearPreview();
//            Debug.Log("Place mode: " + placeMode);
//        }

//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            placeMode = false;
//            ClearPreview();
//        }

//        if (placeMode && selectedConfig != null)
//            UpdatePreview();

//        if (Input.GetMouseButtonUp(0) && !cameraController.IsDragging)
//            HandleClick();
//    }

//    void UpdatePreview()
//    {
//        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        worldPos.z = 0;
//        gridRenderer.GetGridCoordinates(worldPos, out int x, out int y);

//        // Only redraw if the mouse moved to a different tile
//        if (x == previewOriginX && y == previewOriginY) return;

//        ClearPreview();

//        previewOriginX = x;
//        previewOriginY = y;

//        bool valid = IsFootprintValid(x, y);
//        Color highlight = valid ? colorValid : colorInvalid;

//        for (int dx = 0; dx < selectedConfig.footprintWidth; dx++)
//        {
//            for (int dy = 0; dy < selectedConfig.footprintHeight; dy++)
//            {
//                int tx = x + dx;
//                int ty = y + dy;
//                if (gridRenderer.Grid.GetTile(tx, ty) != null)
//                    gridRenderer.SetTileColor(tx, ty, highlight);
//            }
//        }
//    }

//    void ClearPreview()
//    {
//        if (previewOriginX < 0 || selectedConfig == null) return;

//        for (int dx = 0; dx < selectedConfig.footprintWidth; dx++)
//        {
//            for (int dy = 0; dy < selectedConfig.footprintHeight; dy++)
//            {
//                gridRenderer.RefreshTile(previewOriginX + dx, previewOriginY + dy);
//            }
//        }

//        previewOriginX = -1;
//        previewOriginY = -1;
//    }

//    bool IsFootprintValid(int x, int y)
//    {
//        for (int dx = 0; dx < selectedConfig.footprintWidth; dx++)
//        {
//            for (int dy = 0; dy < selectedConfig.footprintHeight; dy++)
//            {
//                TileData tile = gridRenderer.Grid.GetTile(x + dx, y + dy);
//                if (tile == null || tile.tileType != TileType.Empty)
//                    return false;
//            }
//        }
//        return true;
//    }

//    void HandleClick()
//    {
//        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        worldPos.z = 0;
//        gridRenderer.GetGridCoordinates(worldPos, out int x, out int y);

//        if (placeMode)
//        {
//            if (selectedConfig == null) return;

//            bool placed = BuildingManager.Instance.PlaceBuilding(selectedConfig, x, y);
//            if (placed)
//            {
//                ClearPreview();
//                placeMode = false;
//            }
//        }
//        else
//        {
//            TileData tile = gridRenderer.Grid.GetTile(x, y);
//            if (tile == null) return;

//            if (tile.occupant != null)
//            {
//                BuildingState b = tile.occupant;
//                Debug.Log("Selected: " + b.config.buildingName + " Level " + b.level + (b.isUpgrading ? " (upgrading...)" : ""));
//                BuildingManager.Instance.UpgradeBuilding(x, y);
//            }
//            else
//            {
//                Debug.Log("Empty tile: (" + x + ", " + y + ")");
//            }
//        }
//    }

//    public void SetBuilding(BuildingConfig config)
//    {
//        selectedConfig = config;
//        placeMode = true;
//    }
//}
#endregion
#region Phase 1 Bugfix - Building Placer with Footprint Preview
//using UnityEngine;
//using System.Collections.Generic;

//public class BuildingPlacer : MonoBehaviour
//{
//    [SerializeField] private BuildingConfig selectedConfig;

//    private static readonly Color colorValid = new Color(0.2f, 0.9f, 0.2f, 0.6f);
//    private static readonly Color colorInvalid = new Color(0.9f, 0.2f, 0.2f, 0.6f);

//    private bool placeMode;
//    private TileGridRenderer gridRenderer;
//    private CameraController cameraController;

//    // Tracks exactly which tiles were painted so ClearPreview restores the right ones
//    private readonly List<Vector2Int> paintedTiles = new List<Vector2Int>();
//    private int previewOriginX = int.MinValue;
//    private int previewOriginY = int.MinValue;

//    void Start()
//    {
//        gridRenderer = FindFirstObjectByType<TileGridRenderer>();
//        cameraController = FindFirstObjectByType<CameraController>();
//    }

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.B))
//        {
//            placeMode = !placeMode;
//            if (!placeMode) ClearPreview();
//            Debug.Log("Place mode: " + placeMode);
//        }

//        if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            placeMode = false;
//            ClearPreview();
//        }

//        if (placeMode && selectedConfig != null)
//            UpdatePreview();

//        if (Input.GetMouseButtonUp(0) && !cameraController.IsDragging)
//            HandleClick();
//    }

//    void UpdatePreview()
//    {
//        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        worldPos.z = 0;
//        gridRenderer.GetGridCoordinates(worldPos, out int x, out int y);

//        if (x == previewOriginX && y == previewOriginY) return;

//        ClearPreview();

//        previewOriginX = x;
//        previewOriginY = y;

//        bool valid = IsFootprintValid(x, y);
//        Color highlight = valid ? colorValid : colorInvalid;

//        for (int dx = 0; dx < selectedConfig.footprintWidth; dx++)
//        {
//            for (int dy = 0; dy < selectedConfig.footprintHeight; dy++)
//            {
//                int tx = x + dx;
//                int ty = y + dy;
//                if (gridRenderer.Grid.GetTile(tx, ty) != null)
//                {
//                    gridRenderer.SetTileColor(tx, ty, highlight);
//                    paintedTiles.Add(new Vector2Int(tx, ty));
//                }
//            }
//        }
//    }

//    void ClearPreview()
//    {
//        foreach (Vector2Int tile in paintedTiles)
//            gridRenderer.RefreshTile(tile.x, tile.y);

//        paintedTiles.Clear();
//        previewOriginX = int.MinValue;
//        previewOriginY = int.MinValue;
//    }

//    bool IsFootprintValid(int x, int y)
//    {
//        for (int dx = 0; dx < selectedConfig.footprintWidth; dx++)
//        {
//            for (int dy = 0; dy < selectedConfig.footprintHeight; dy++)
//            {
//                TileData tile = gridRenderer.Grid.GetTile(x + dx, y + dy);
//                if (tile == null || tile.tileType != TileType.Empty)
//                    return false;
//            }
//        }
//        return true;
//    }

//    void HandleClick()
//    {
//        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        worldPos.z = 0;
//        gridRenderer.GetGridCoordinates(worldPos, out int x, out int y);

//        if (placeMode)
//        {
//            if (selectedConfig == null) return;

//            bool placed = BuildingManager.Instance.PlaceBuilding(selectedConfig, x, y);
//            if (placed)
//            {
//                ClearPreview();
//                placeMode = false;
//            }
//        }
//        else
//        {
//            TileData tile = gridRenderer.Grid.GetTile(x, y);
//            if (tile == null) return;

//            if (tile.occupant != null)
//            {
//                BuildingState b = tile.occupant;
//                Debug.Log("Selected: " + b.config.buildingName + " Level " + b.level + (b.isUpgrading ? " (upgrading...)" : ""));
//                BuildingManager.Instance.UpgradeBuilding(x, y);
//            }
//            else
//            {
//                Debug.Log("Empty tile: (" + x + ", " + y + ")");
//            }
//        }
//    }

//    public void SetBuilding(BuildingConfig config)
//    {
//        selectedConfig = config;
//        placeMode = true;
//    }
//}
#endregion

#region Phase 2 Sprint 3 - Building Placer With Interaction Popup
using UnityEngine;
using System.Collections.Generic;
public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingConfig selectedConfig;
    private static readonly Color colorValid = new Color(0.2f, 0.9f, 0.2f, 0.6f);
    private static readonly Color colorInvalid = new Color(0.9f, 0.2f, 0.2f, 0.6f);
    private bool placeMode;
    private TileGridRenderer gridRenderer;
    private CameraController cameraController;
    private readonly List<Vector2Int> paintedTiles = new List<Vector2Int>();
    private int previewOriginX = int.MinValue;
    private int previewOriginY = int.MinValue;

    void Start()
    {
        gridRenderer = FindFirstObjectByType<TileGridRenderer>();
        cameraController = FindFirstObjectByType<CameraController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            placeMode = !placeMode;
            if (!placeMode) ClearPreview();
            Debug.Log("Place mode: " + placeMode);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            placeMode = false;
            ClearPreview();
            BuildingInteraction.Instance.Deselect();
        }

        if (placeMode && selectedConfig != null)
            UpdatePreview();

        if (Input.GetMouseButtonUp(0) && !cameraController.IsDragging)
            HandleClick();
    }

    void UpdatePreview()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        gridRenderer.GetGridCoordinates(worldPos, out int x, out int y);
        if (x == previewOriginX && y == previewOriginY) return;
        ClearPreview();
        previewOriginX = x;
        previewOriginY = y;
        bool valid = IsFootprintValid(x, y);
        Color highlight = valid ? colorValid : colorInvalid;
        for (int dx = 0; dx < selectedConfig.footprintWidth; dx++)
        {
            for (int dy = 0; dy < selectedConfig.footprintHeight; dy++)
            {
                int tx = x + dx;
                int ty = y + dy;
                if (gridRenderer.Grid.GetTile(tx, ty) != null)
                {
                    gridRenderer.SetTileColor(tx, ty, highlight);
                    paintedTiles.Add(new Vector2Int(tx, ty));
                }
            }
        }
    }

    void ClearPreview()
    {
        foreach (Vector2Int tile in paintedTiles)
            gridRenderer.RefreshTile(tile.x, tile.y);
        paintedTiles.Clear();
        previewOriginX = int.MinValue;
        previewOriginY = int.MinValue;
    }

    bool IsFootprintValid(int x, int y)
    {
        for (int dx = 0; dx < selectedConfig.footprintWidth; dx++)
        {
            for (int dy = 0; dy < selectedConfig.footprintHeight; dy++)
            {
                TileData tile = gridRenderer.Grid.GetTile(x + dx, y + dy);
                if (tile == null || tile.tileType != TileType.Empty)
                    return false;
            }
        }
        return true;
    }

    void HandleClick()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        gridRenderer.GetGridCoordinates(worldPos, out int x, out int y);

        if (placeMode)
        {
            if (selectedConfig == null) return;
            bool placed = BuildingManager.Instance.PlaceBuilding(selectedConfig, x, y);
            if (placed)
            {
                ClearPreview();
                placeMode = false;
            }
        }
        else
        {
            TileData tile = gridRenderer.Grid.GetTile(x, y);
            if (tile == null) return;

            if (tile.occupant != null)
            {
                // Open popup instead of auto-upgrading
                BuildingInteraction.Instance.SelectBuilding(tile.occupant);
            }
            else
            {
                // Tapped empty ground — close popup
                BuildingInteraction.Instance.Deselect();
            }
        }
    }

    public void SetBuilding(BuildingConfig config)
    {
        selectedConfig = config;
        placeMode = true;
    }
}
#endregion
