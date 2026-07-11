#region Summary
/// <summary>
/// BanditCamp is a class that represents the runtime data for a bandit camp tile in the game. 
///     It contains information about the camp's level, combat power, whether it has been cleared by the player, and a respawn timer for when it can reappear after being cleared. 
///     Each BanditCamp instance is associated with specific tile coordinates (tileX and tileY) on the game map. This class is managed by the BanditCampManager and is stored within TileData for each relevant tile.
/// Example usage:
/// 1. When a new bandit camp is generated on the map, a BanditCamp instance is created with a specific level and tile coordinates.
/// 2. When the player engages in combat with a bandit camp, the combat power can be accessed from the BanditCamp instance to determine the difficulty of the encounter.
/// 3. After the player defeats a bandit camp, the isCleared property can be set to true, and the respawnTimer can be initialized to start counting down until the camp can reappear.
/// Note: The level of the bandit camp determines its combat power, with higher levels being more challenging. The respawn timer allows for dynamic gameplay, as cleared camps can eventually return, providing ongoing challenges for the player.
/// </summary>
#endregion

#region Milestone 1 sprint 10 - Bandit Camp Class
using UnityEngine;

// Runtime data for one bandit camp tile.
// Lives on TileData. Managed by BanditCampManager.
public class BanditCamp
{
    public int level;           // 1-6
    public float power;         // Combat power, scales with level
    public bool isCleared;      // True after player wins
    public float respawnTimer;  // Counts down in seconds after cleared
    public int tileX;
    public int tileY;

    public BanditCamp(int level, int tileX, int tileY)
    {
        this.level = level;
        this.tileX = tileX;
        this.tileY = tileY;
        this.power = level * 150f; // Lvl1=150, Lvl6=900
        this.isCleared = false;
    }
}
#endregion