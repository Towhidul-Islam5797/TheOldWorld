#region Summary
/// <summary>
/// BuildingManager is a central class responsible
/// for managing all building-related operations in the game, including placement, upgrades, and tracking of building states.
/// It interacts with the TileGrid to determine where buildings can be placed and with the BuildingState class to manage individual building levels and upgrade processes.
/// Key functions of the BuildingManager include:
/// 1. Placing buildings on the grid based on player input and ensuring that the tile is available for construction.
/// 2. Upgrading existing buildings by checking the current level and the HQ level to determine if an upgrade is possible, and then initiating the upgrade process.
/// 3. Keeping track of all buildings in the game through a list of BuildingState instances, allowing for easy access and management of building data.
/// This class is essential for the core gameplay mechanics related to building management and serves as a bridge between the player’s actions and the underlying game systems that govern building behavior and interactions.
/// Example usage:
/// - When a player attempts to place a building, the BuildingManager checks the tile's availability and updates the grid and building list accordingly.
/// - When a player attempts to upgrade a building, the BuildingManager checks the building's current level and the HQ level to determine if the upgrade can proceed, and if so,
///         it initiates the upgrade process and updates the building's state.
/// Note: The BuildingManager relies on other classes such as TileGrid, TileGridRenderer, BuildingState, and BuildingConfig to function properly, 
///         and it assumes that these classes are implemented with the necessary properties and methods to support building management operations.
#endregion
#region Phase 1 Sprint 3 - Building Manager Implementation
using UnityEngine;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    private TileGrid grid;
    private TileGridRenderer gridRenderer;
    private List<BuildingState> allBuildings = new List<BuildingState>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gridRenderer = FindFirstObjectByType<TileGridRenderer>();
        grid = gridRenderer.Grid;
    }

    public int GetHQLevel()
    {
        foreach (BuildingState b in allBuildings)
        {
            if (b.config.buildingType == BuildingType.HQ)
                return b.level;
        }
        return 0;
    }

    public bool PlaceBuilding(BuildingConfig config, int x, int y)
    {
        TileData tile = grid.GetTile(x, y);
        if (tile == null || tile.tileType != TileType.Empty)
        {
            Debug.Log("Cannot place: tile unavailable at (" + x + ", " + y + ")");
            return false;
        }

        BuildingState building = new BuildingState(config, x, y);
        tile.tileType = TileType.Occupied;
        tile.occupant = building;
        allBuildings.Add(building);
        gridRenderer.RefreshTile(x, y);

        Debug.Log("Placed " + config.buildingName + " at (" + x + ", " + y + ")");
        return true;
    }

    public bool UpgradeBuilding(int x, int y)
    {
        TileData tile = grid.GetTile(x, y);
        if (tile == null || tile.occupant == null) return false;

        BuildingState building = tile.occupant;
        int hqLevel = GetHQLevel();

        if (!building.CanUpgrade(hqLevel))
        {
            Debug.Log("Cannot upgrade " + building.config.buildingName + " level " + building.level);
            return false;
        }

        building.StartUpgrade();
        Debug.Log("Upgrading " + building.config.buildingName + " to level " + (building.level + 1));
        return true;
    }

    void Update()
    {
        foreach (BuildingState b in allBuildings)
            b.CheckUpgradeComplete();
    }
}
#endregion