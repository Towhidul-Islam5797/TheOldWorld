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
//using UnityEngine;
//using System.Collections.Generic;

//public class BuildingManager : MonoBehaviour
//{
//    public static BuildingManager Instance;

//    private TileGrid grid;
//    private TileGridRenderer gridRenderer;
//    private List<BuildingState> allBuildings = new List<BuildingState>();

//    void Awake()
//    {
//        Instance = this;
//    }

//    void Start()
//    {
//        gridRenderer = FindFirstObjectByType<TileGridRenderer>();
//        grid = gridRenderer.Grid;
//    }

//    public int GetHQLevel()
//    {
//        foreach (BuildingState b in allBuildings)
//        {
//            if (b.config.buildingType == BuildingType.HQ)
//                return b.level;
//        }
//        return 0;
//    }

//    public bool PlaceBuilding(BuildingConfig config, int x, int y)
//    {
//        TileData tile = grid.GetTile(x, y);
//        if (tile == null || tile.tileType != TileType.Empty)
//        {
//            Debug.Log("Cannot place: tile unavailable at (" + x + ", " + y + ")");
//            return false;
//        }

//        BuildingState building = new BuildingState(config, x, y);
//        tile.tileType = TileType.Occupied;
//        tile.occupant = building;
//        allBuildings.Add(building);
//        gridRenderer.RefreshTile(x, y);

//        Debug.Log("Placed " + config.buildingName + " at (" + x + ", " + y + ")");
//        return true;
//    }

//    public bool UpgradeBuilding(int x, int y)
//    {
//        TileData tile = grid.GetTile(x, y);
//        if (tile == null || tile.occupant == null) return false;

//        BuildingState building = tile.occupant;
//        int hqLevel = GetHQLevel();

//        if (!building.CanUpgrade(hqLevel))
//        {
//            Debug.Log("Cannot upgrade " + building.config.buildingName + " level " + building.level);
//            return false;
//        }

//        building.StartUpgrade();
//        Debug.Log("Upgrading " + building.config.buildingName + " to level " + (building.level + 1));
//        return true;
//    }

//    void Update()
//    {
//        foreach (BuildingState b in allBuildings)
//            b.CheckUpgradeComplete();
//    }
//}
#endregion
#region Phase 2 Sprint 4 - Building Manager Extended
//using System.Collections.Generic;
//using System.Resources;
//using UnityEngine;

//public class BuildingManager : MonoBehaviour
//{
//    public static BuildingManager Instance;

//    private TileGrid grid;
//    private TileGridRenderer gridRenderer;
//    private List<BuildingState> allBuildings = new List<BuildingState>();

//    void Awake()
//    {
//        Instance = this;
//    }

//    void Start()
//    {
//        gridRenderer = FindFirstObjectByType<TileGridRenderer>();
//        grid = gridRenderer.Grid;
//    }

//    public int GetHQLevel()
//    {
//        foreach (BuildingState b in allBuildings)
//        {
//            if (b.config.buildingType == BuildingType.HQ)
//                return b.level;
//        }
//        return 0;
//    }

//    public List<BuildingState> GetAllBuildings()
//    {
//        return allBuildings;
//    }

//    public bool PlaceBuilding(BuildingConfig config, int x, int y)
//    {
//        TileData tile = grid.GetTile(x, y);
//        if (tile == null || tile.tileType != TileType.Empty)
//        {
//            Debug.Log("Cannot place: tile unavailable at (" + x + ", " + y + ")");
//            return false;
//        }

//        if (!ResourceManager.Instance.CanAfford(config.placementCost))
//        {
//            Debug.Log("Cannot place: not enough resources");
//            return false;
//        }

//        ResourceManager.Instance.Deduct(config.placementCost);

//        BuildingState building = new BuildingState(config, x, y);
//        tile.tileType = TileType.Occupied;
//        tile.occupant = building;
//        allBuildings.Add(building);
//        gridRenderer.RefreshTile(x, y);

//        Debug.Log("Placed " + config.buildingName + " at (" + x + ", " + y + ")");
//        return true;
//    }

//    public bool UpgradeBuilding(int x, int y)
//    {
//        TileData tile = grid.GetTile(x, y);
//        if (tile == null || tile.occupant == null) return false;

//        BuildingState building = tile.occupant;
//        int hqLevel = GetHQLevel();

//        if (!building.CanUpgrade(hqLevel))
//        {
//            Debug.Log("Cannot upgrade " + building.config.buildingName + " level " + building.level);
//            return false;
//        }

//        if (!ResourceManager.Instance.CanAfford(building.config.upgradeCost))
//        {
//            Debug.Log("Cannot upgrade: not enough resources");
//            return false;
//        }

//        ResourceManager.Instance.Deduct(building.config.upgradeCost);
//        building.StartUpgrade();
//        Debug.Log("Upgrading " + building.config.buildingName + " to level " + (building.level + 1));
//        return true;
//    }

//    void Update()
//    {
//        foreach (BuildingState b in allBuildings)
//            b.CheckUpgradeComplete();
//    }
//}
#endregion
#region Phase 2 Sprint 7 - Building Manager with Visuals
//using System.Collections.Generic;
//using System.Resources;
//using UnityEngine;

//public class BuildingManager : MonoBehaviour
//{
//    public static BuildingManager Instance;

//    private TileGrid grid;
//    private TileGridRenderer gridRenderer;
//    private List<BuildingState> allBuildings = new List<BuildingState>();

//    void Awake()
//    {
//        Instance = this;
//    }

//    void Start()
//    {
//        gridRenderer = FindFirstObjectByType<TileGridRenderer>();
//        grid = gridRenderer.Grid;
//    }

//    public int GetHQLevel()
//    {
//        foreach (BuildingState b in allBuildings)
//        {
//            if (b.config.buildingType == BuildingType.HQ)
//                return b.level;
//        }
//        return 0;
//    }

//    public List<BuildingState> GetAllBuildings()
//    {
//        return allBuildings;
//    }

//    public bool PlaceBuilding(BuildingConfig config, int x, int y)
//    {
//        // Check all footprint tiles are empty
//        for (int dx = 0; dx < config.footprintWidth; dx++)
//        {
//            for (int dy = 0; dy < config.footprintHeight; dy++)
//            {
//                TileData tile = grid.GetTile(x + dx, y + dy);
//                if (tile == null || tile.tileType != TileType.Empty)
//                {
//                    Debug.Log("Cannot place: tile unavailable at (" + (x + dx) + ", " + (y + dy) + ")");
//                    return false;
//                }
//            }
//        }

//        if (!ResourceManager.Instance.CanAfford(config.placementCost))
//        {
//            Debug.Log("Cannot place: not enough resources");
//            return false;
//        }

//        ResourceManager.Instance.Deduct(config.placementCost);

//        BuildingState building = new BuildingState(config, x, y);

//        // Mark all footprint tiles as occupied
//        for (int dx = 0; dx < config.footprintWidth; dx++)
//        {
//            for (int dy = 0; dy < config.footprintHeight; dy++)
//            {
//                TileData tile = grid.GetTile(x + dx, y + dy);
//                tile.tileType = TileType.Occupied;
//                tile.occupant = building;
//                gridRenderer.RefreshTile(x + dx, y + dy);
//            }
//        }

//        allBuildings.Add(building);

//        if (config.buildingSprite != null)
//            SpawnBuildingSprite(config, x, y);

//        Debug.Log("Placed " + config.buildingName + " at (" + x + ", " + y + ")");
//        return true;
//    }

//    private void SpawnBuildingSprite(BuildingConfig config, int x, int y)
//    {
//        // Get world positions of the two corner tiles to find the footprint center
//        Vector3 bottomLeft = gridRenderer.GridToWorld(x, y);
//        Vector3 topRight = gridRenderer.GridToWorld(x + config.footprintWidth - 1, y + config.footprintHeight - 1);
//        Vector3 center = (bottomLeft + topRight) * 0.5f;

//        GameObject spriteObj = new GameObject("BuildingSprite_" + config.buildingName);
//        spriteObj.transform.position = center;

//        SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
//        sr.sprite = config.buildingSprite;
//        sr.sortingLayerName = "Buildings";
//        // Buildings lower on screen draw in front of buildings higher up
//        sr.sortingOrder = -(int)(center.y * 100);
//    }

//    public bool UpgradeBuilding(int x, int y)
//    {
//        TileData tile = grid.GetTile(x, y);
//        if (tile == null || tile.occupant == null) return false;

//        BuildingState building = tile.occupant;
//        int hqLevel = GetHQLevel();

//        if (!building.CanUpgrade(hqLevel))
//        {
//            Debug.Log("Cannot upgrade " + building.config.buildingName + " level " + building.level);
//            return false;
//        }

//        if (!ResourceManager.Instance.CanAfford(building.config.upgradeCost))
//        {
//            Debug.Log("Cannot upgrade: not enough resources");
//            return false;
//        }

//        ResourceManager.Instance.Deduct(building.config.upgradeCost);
//        building.StartUpgrade();
//        Debug.Log("Upgrading " + building.config.buildingName + " to level " + (building.level + 1));
//        return true;
//    }

//    void Update()
//    {
//        foreach (BuildingState b in allBuildings)
//            b.CheckUpgradeComplete();
//    }
//}
#endregion

#region Phase 2 Sprint 1 - Building Manager With Level Sprites
//using System.Collections.Generic;
//using UnityEngine;

//public class BuildingManager : MonoBehaviour
//{
//    public static BuildingManager Instance;

//    private TileGrid grid;
//    private TileGridRenderer gridRenderer;
//    private List<BuildingState> allBuildings = new List<BuildingState>();

//    // Tracks the sprite GameObject for each building so we can swap it on upgrade
//    private Dictionary<BuildingState, GameObject> buildingSprites = new Dictionary<BuildingState, GameObject>();

//    void Awake()
//    {
//        Instance = this;
//    }

//    void Start()
//    {
//        gridRenderer = FindFirstObjectByType<TileGridRenderer>();
//        grid = gridRenderer.Grid;
//    }

//    public int GetHQLevel()
//    {
//        foreach (BuildingState b in allBuildings)
//            if (b.config.buildingType == BuildingType.HQ) return b.level;
//        return 0;
//    }

//    public List<BuildingState> GetAllBuildings()
//    {
//        return allBuildings;
//    }

//    public bool PlaceBuilding(BuildingConfig config, int x, int y)
//    {
//        for (int dx = 0; dx < config.footprintWidth; dx++)
//        {
//            for (int dy = 0; dy < config.footprintHeight; dy++)
//            {
//                TileData tile = grid.GetTile(x + dx, y + dy);
//                if (tile == null || tile.tileType != TileType.Empty)
//                {
//                    Debug.Log("Cannot place: tile unavailable at (" + (x + dx) + ", " + (y + dy) + ")");
//                    return false;
//                }
//            }
//        }

//        if (!ResourceManager.Instance.CanAfford(config.placementCost))
//        {
//            Debug.Log("Cannot place: not enough resources");
//            return false;
//        }

//        ResourceManager.Instance.Deduct(config.placementCost);

//        BuildingState building = new BuildingState(config, x, y);

//        for (int dx = 0; dx < config.footprintWidth; dx++)
//        {
//            for (int dy = 0; dy < config.footprintHeight; dy++)
//            {
//                TileData tile = grid.GetTile(x + dx, y + dy);
//                tile.tileType = TileType.Occupied;
//                tile.occupant = building;
//                gridRenderer.RefreshTile(x + dx, y + dy);
//            }
//        }

//        allBuildings.Add(building);
//        SpawnBuildingSprite(building, x, y);

//        Debug.Log("Placed " + config.buildingName + " at (" + x + ", " + y + ")");
//        return true;
//    }

//    private void SpawnBuildingSprite(BuildingState building, int x, int y)
//    {
//        Sprite sprite = building.config.GetLevel(building.level).sprite;
//        if (sprite == null) return;

//        Vector3 bottomLeft = gridRenderer.GridToWorld(x, y);
//        Vector3 topRight = gridRenderer.GridToWorld(
//            x + building.config.footprintWidth - 1,
//            y + building.config.footprintHeight - 1);
//        Vector3 center = (bottomLeft + topRight) * 0.5f;

//        GameObject spriteObj = new GameObject("BuildingSprite_" + building.config.buildingName);
//        spriteObj.transform.position = center;

//        SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
//        sr.sprite = sprite;
//        sr.sortingLayerName = "Buildings";
//        sr.sortingOrder = -(int)(center.y * 100);

//        buildingSprites[building] = spriteObj;
//    }

//    private void UpdateBuildingSprite(BuildingState building)
//    {
//        if (!buildingSprites.ContainsKey(building)) return;
//        Sprite sprite = building.config.GetLevel(building.level).sprite;
//        if (sprite == null) return;
//        buildingSprites[building].GetComponent<SpriteRenderer>().sprite = sprite;
//    }

//    public bool UpgradeBuilding(int x, int y)
//    {
//        TileData tile = grid.GetTile(x, y);
//        if (tile == null || tile.occupant == null) return false;

//        BuildingState building = tile.occupant;
//        int hqLevel = GetHQLevel();

//        if (!building.CanUpgrade(hqLevel))
//        {
//            Debug.Log("Cannot upgrade " + building.config.buildingName + " level " + building.level);
//            return false;
//        }

//        ResourceCost upgradeCost = building.config.GetLevel(building.level).upgradeCost;
//        if (!ResourceManager.Instance.CanAfford(upgradeCost))
//        {
//            Debug.Log("Cannot upgrade: not enough resources");
//            return false;
//        }

//        ResourceManager.Instance.Deduct(upgradeCost);
//        building.StartUpgrade();

//        Debug.Log("Upgrading " + building.config.buildingName + " to level " + (building.level + 1));
//        return true;
//    }

//    void Update()
//    {
//        foreach (BuildingState b in allBuildings)
//        {
//            bool wasUpgrading = b.isUpgrading;
//            b.CheckUpgradeComplete();
//            if (wasUpgrading && !b.isUpgrading)
//                UpdateBuildingSprite(b);
//        }
//    }
//}
#endregion

#region Phase 2 Sprint 3 - Building Manager With Popup Support
using System.Collections.Generic;
using UnityEngine;
public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;
    private TileGrid grid;
    private TileGridRenderer gridRenderer;
    private List<BuildingState> allBuildings = new List<BuildingState>();
    private Dictionary<BuildingState, GameObject> buildingSprites = new Dictionary<BuildingState, GameObject>();

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
            if (b.config.buildingType == BuildingType.HQ) return b.level;
        return 0;
    }

    public List<BuildingState> GetAllBuildings()
    {
        return allBuildings;
    }

    public BuildingState GetBuildingAt(int x, int y)
    {
        TileData tile = grid.GetTile(x, y);
        if (tile == null) return null;
        return tile.occupant;
    }

    public Vector3 GetBuildingWorldPosition(BuildingState building)
    {
        if (!buildingSprites.ContainsKey(building)) return Vector3.zero;
        return buildingSprites[building].transform.position;
    }

    public bool PlaceBuilding(BuildingConfig config, int x, int y)
    {
        for (int dx = 0; dx < config.footprintWidth; dx++)
        {
            for (int dy = 0; dy < config.footprintHeight; dy++)
            {
                TileData tile = grid.GetTile(x + dx, y + dy);
                if (tile == null || tile.tileType != TileType.Empty)
                {
                    Debug.Log("Cannot place: tile unavailable at (" + (x + dx) + ", " + (y + dy) + ")");
                    return false;
                }
            }
        }

        if (!ResourceManager.Instance.CanAfford(config.placementCost))
        {
            Debug.Log("Cannot place: not enough resources");
            return false;
        }

        ResourceManager.Instance.Deduct(config.placementCost);
        BuildingState building = new BuildingState(config, x, y);

        for (int dx = 0; dx < config.footprintWidth; dx++)
        {
            for (int dy = 0; dy < config.footprintHeight; dy++)
            {
                TileData tile = grid.GetTile(x + dx, y + dy);
                tile.tileType = TileType.Occupied;
                tile.occupant = building;
                gridRenderer.RefreshTile(x + dx, y + dy);
            }
        }

        allBuildings.Add(building);
        SpawnBuildingSprite(building, x, y);
        Debug.Log("Placed " + config.buildingName + " at (" + x + ", " + y + ")");
        return true;
    }

    private void SpawnBuildingSprite(BuildingState building, int x, int y)
    {
        Sprite sprite = building.config.GetLevel(building.level).sprite;
        if (sprite == null) return;

        Vector3 bottomLeft = gridRenderer.GridToWorld(x, y);
        Vector3 topRight = gridRenderer.GridToWorld(
            x + building.config.footprintWidth - 1,
            y + building.config.footprintHeight - 1);
        Vector3 center = (bottomLeft + topRight) * 0.5f;

        GameObject spriteObj = new GameObject("BuildingSprite_" + building.config.buildingName);
        spriteObj.transform.position = center;
        SpriteRenderer sr = spriteObj.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingLayerName = "Buildings";
        sr.sortingOrder = -(int)(center.y * 100);
        buildingSprites[building] = spriteObj;
    }

    private void UpdateBuildingSprite(BuildingState building)
    {
        if (!buildingSprites.ContainsKey(building)) return;
        Sprite sprite = building.config.GetLevel(building.level).sprite;
        if (sprite == null) return;
        buildingSprites[building].GetComponent<SpriteRenderer>().sprite = sprite;
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

        ResourceCost upgradeCost = building.config.GetLevel(building.level).upgradeCost;
        if (!ResourceManager.Instance.CanAfford(upgradeCost))
        {
            Debug.Log("Cannot upgrade: not enough resources");
            return false;
        }

        ResourceManager.Instance.Deduct(upgradeCost);
        building.StartUpgrade();
        Debug.Log("Upgrading " + building.config.buildingName + " to level " + (building.level + 1));
        return true;
    }

    public void CancelUpgrade(int x, int y)
    {
        TileData tile = grid.GetTile(x, y);
        if (tile == null || tile.occupant == null) return;
        BuildingState building = tile.occupant;
        if (!building.isUpgrading) return;

        // Refund the upgrade cost resources one by one
        ResourceCost cost = building.config.GetLevel(building.level).upgradeCost;
        ResourceManager.Instance.Add(ResourceType.Food, cost.food);
        ResourceManager.Instance.Add(ResourceType.Wood, cost.wood);
        ResourceManager.Instance.Add(ResourceType.Stone, cost.stone);
        ResourceManager.Instance.Add(ResourceType.Silver, cost.silver);

        building.CancelUpgrade();
        Debug.Log("Cancelled upgrade on " + building.config.buildingName);
    }

    void Update()
    {
        foreach (BuildingState b in allBuildings)
        {
            bool wasUpgrading = b.isUpgrading;
            b.CheckUpgradeComplete();
            if (wasUpgrading && !b.isUpgrading)
                UpdateBuildingSprite(b);
        }
    }
}
#endregion