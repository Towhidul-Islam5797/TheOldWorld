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
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingConfig selectedConfig;

    private bool placeMode;
    private TileGridRenderer gridRenderer;
    private CameraController cameraController;

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
            Debug.Log("Place mode: " + placeMode);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            placeMode = false;

        if (Input.GetMouseButtonUp(0) && !cameraController.IsDragging)
            HandleClick();
    }

    void HandleClick()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;

        gridRenderer.GetGridCoordinates(worldPos, out int x, out int y);

        if (placeMode)
        {
            if (selectedConfig != null)
                BuildingManager.Instance.PlaceBuilding(selectedConfig, x, y);
        }
        else
        {
            TileData tile = gridRenderer.Grid.GetTile(x, y);
            if (tile == null) return;

            if (tile.occupant != null)
            {
                BuildingState b = tile.occupant;
                Debug.Log("Selected: " + b.config.buildingName + " Level " + b.level + (b.isUpgrading ? " (upgrading...)" : ""));
                BuildingManager.Instance.UpgradeBuilding(x, y);
            }
            else
            {
                Debug.Log("Empty tile: (" + x + ", " + y + ")");
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