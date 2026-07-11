#region Summary
/// <summary>
/// BanditCampManager is a MonoBehaviour that manages the lifecycle of bandit camps in the game.
///    It is responsible for spawning camps at game start, handling player attacks on camps, resolving combat outcomes, delivering loot, and managing camp respawn timers.
///    The manager interacts with the TileGrid to place camps on the map and updates the TileGridRenderer to reflect changes in camp status.
///    When a player attacks a camp, the manager checks the player's available troops, resolves combat using the CombatResolver, deducts lost troops from the inventory, and awards resources and crafting materials if the player wins.
///    Cleared camps are marked and start a respawn timer, after which they automatically reappear on the map for future encounters.
/// Example usage:
/// 1. At the start of the game, BanditCampManager spawns a set number of bandit camps across the grid based on the configuration defined in BanditCampConfig.
/// 2. When a player clicks on a tile with an NPCCamp and confirms an attack, the AttackCamp method is called, which processes the combat and loot logic.
/// 3. The Update method continuously checks for cleared camps and ticks down their respawn timers, respawning them when the timer reaches zero.
/// Note: The BanditCampManager relies on other components such as TileGrid, TileGridRenderer, TroopInventory, ResourceManager, and CombatResolver to function properly. 
///     It serves as the central hub for all bandit camp-related interactions and state management in the game.
/// </summary>
#endregion 

#region Milestone 1 Sprint 10 - Bandit Camp Manager
// Spawns bandit camps on the grid at game start.
// Handles player attacks: resolves combat, delivers loot, starts respawn timer.
// Camps respawn automatically after respawnTimeSeconds.

using UnityEngine;
using System.Collections.Generic;
public class BanditCampManager : MonoBehaviour
{
    public static BanditCampManager Instance;

    [SerializeField] private BanditCampConfig config;

    private TileGrid grid;
    private TileGridRenderer gridRenderer;

    private List<BanditCamp> allCamps = new List<BanditCamp>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gridRenderer = FindFirstObjectByType<TileGridRenderer>();
        StartCoroutine(SpawnAfterGrid());
    }

    System.Collections.IEnumerator SpawnAfterGrid()
    {
        // Wait one frame so TileGridRenderer has finished building the grid
        yield return null;
        grid = gridRenderer.Grid;
        SpawnCamps();
    }

    void Update()
    {
        TickRespawnTimers();
    }

    // Places camps randomly across the grid — 2 camps per level (12 total for Lvl1-6)
    void SpawnCamps()
    {
        int campsPerLevel = Mathf.Max(1, config.totalCamps / 6);

        for (int level = 1; level <= 6; level++)
        {
            for (int i = 0; i < campsPerLevel; i++)
            {
                TileData tile = GetRandomEmptyTile();
                if (tile == null) continue;
                PlaceCamp(level, tile.gridX, tile.gridY);
            }
        }

        Debug.Log("BanditCampManager: Spawned " + allCamps.Count + " bandit camps.");
    }

    void PlaceCamp(int level, int x, int y)
    {
        TileData tile = grid.GetTile(x, y);
        if (tile == null || tile.tileType != TileType.Empty) return;

        BanditCamp camp = new BanditCamp(level, x, y);
        tile.tileType = TileType.NPCCamp;
        tile.campData = camp;
        allCamps.Add(camp);
        gridRenderer.RefreshTile(x, y);
    }

    // Called by player input when they click an NPCCamp tile and confirm attack
    public void AttackCamp(int tileX, int tileY)
    {
        TileData tile = grid.GetTile(tileX, tileY);
        if (tile == null || tile.tileType != TileType.NPCCamp || tile.campData == null)
        {
            Debug.Log("No bandit camp at this tile.");
            return;
        }

        BanditCamp camp = tile.campData;
        if (camp.isCleared)
        {
            Debug.Log("Camp is cleared and respawning. Wait " +
                      Mathf.CeilToInt(camp.respawnTimer) + "s.");
            return;
        }

        // Get current troop counts from inventory
        int infantry = TroopInventory.Instance.GetCount(TroopType.Infantry);
        int archers = TroopInventory.Instance.GetCount(TroopType.Archers);
        int cavalry = TroopInventory.Instance.GetCount(TroopType.Cavalry);
        int siege = TroopInventory.Instance.GetCount(TroopType.Siege);

        int totalTroops = infantry + archers + cavalry + siege;
        if (totalTroops == 0)
        {
            Debug.Log("No troops available to attack.");
            return;
        }

        // Resolve battle
        BattleReport report = CombatResolver.Resolve(infantry, archers, cavalry, siege, camp);

        // Deduct lost troops
        TroopInventory.Instance.Deduct(TroopType.Infantry, report.infantryLost);
        TroopInventory.Instance.Deduct(TroopType.Archers, report.archersLost);
        TroopInventory.Instance.Deduct(TroopType.Cavalry, report.cavalryLost);
        TroopInventory.Instance.Deduct(TroopType.Siege, report.siegeLost);

        if (report.playerWon)
        {
            // Roll and deliver loot
            config.RollResourceLoot(camp.level,
                out report.foodLooted, out report.woodLooted,
                out report.stoneLooted, out report.silverLooted);

            ResourceManager.Instance.Add(ResourceType.Food, report.foodLooted);
            ResourceManager.Instance.Add(ResourceType.Wood, report.woodLooted);
            ResourceManager.Instance.Add(ResourceType.Stone, report.stoneLooted);
            ResourceManager.Instance.Add(ResourceType.Silver, report.silverLooted);

            // Roll crafting material drop
            string material = config.RollMaterialDrop(camp.level);
            if (material != null)
            {
                report.materialsLooted[material] = 1;
                MaterialInventory.Instance.Add(material, 1);
                Debug.Log("Crafting material dropped: " + material + " Lvl" + camp.level);
            }

            // Clear camp and start respawn timer
            camp.isCleared = true;
            camp.respawnTimer = config.respawnTimeSeconds;
            tile.tileType = TileType.Empty;
            tile.campData = null;
            gridRenderer.RefreshTile(tileX, tileY);
        }

        Debug.Log(report.ToString());
    }

    void TickRespawnTimers()
    {
        foreach (BanditCamp camp in allCamps)
        {
            if (!camp.isCleared) continue;

            camp.respawnTimer -= Time.deltaTime;

            if (camp.respawnTimer <= 0f)
            {
                RespawnCamp(camp);
            }
        }
    }

    void RespawnCamp(BanditCamp camp)
    {
        TileData tile = grid.GetTile(camp.tileX, camp.tileY);
        if (tile == null || tile.tileType != TileType.Empty) return;

        camp.isCleared = false;
        camp.respawnTimer = 0f;
        tile.tileType = TileType.NPCCamp;
        tile.campData = camp;
        gridRenderer.RefreshTile(camp.tileX, camp.tileY);

        Debug.Log("Bandit camp respawned at (" + camp.tileX + ", " + camp.tileY +
                  ") Level " + camp.level);
    }

    TileData GetRandomEmptyTile()
    {
        int attempts = 100;
        while (attempts-- > 0)
        {
            int x = Random.Range(0, grid.width);
            int y = Random.Range(0, grid.height);
            TileData tile = grid.GetTile(x, y);
            if (tile != null && tile.tileType == TileType.Empty)
                return tile;
        }
        return null;
    }

    // Returns camp data for a tile — used by UI/input to show camp info
    public BanditCamp GetCampAt(int x, int y)
    {
        TileData tile = grid.GetTile(x, y);
        if (tile == null || tile.tileType != TileType.NPCCamp) return null;
        return tile.campData;
    }
}
#endregion
