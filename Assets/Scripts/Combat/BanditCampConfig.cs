#region Summary
/// <summary>
/// BanditCampConfig is a ScriptableObject that holds configuration data for bandit camps in the game. 
///     It defines settings for how many camps to spawn, how long they take to respawn after being cleared, and the ranges for resource loot that players can obtain from defeating camps. 
///     Additionally, it includes a method for determining if a crafting material drops when a camp is cleared, based on the camp's level and a base drop chance.
/// Example usage:
/// 1. A BanditCampConfig asset can be created in the Unity editor and populated with desired values for spawn settings and loot ranges.
/// 2. When a bandit camp is cleared by the player, the RollResourceLoot method can be called to determine how much of each resource (food, wood, stone, silver) the player receives, scaled by the camp's level.
/// 3. The RollMaterialDrop method can be called to check if a crafting material drops, and if so, which material it is, based on the camp's level and the defined drop chance.
/// Note: The loot ranges and drop chances can be adjusted in the editor to balance gameplay and provide an appropriate level of reward for defeating bandit camps of varying difficulty levels.
/// </summary>
#endregion

#region Milestone 1 sprint 10 - Bandit Camp Config ScriptableObject
using UnityEngine;

[CreateAssetMenu(fileName = "BanditCampConfig", menuName = "TheOldWorld/BanditCampConfig")]
public class BanditCampConfig : ScriptableObject
{
    [Header("Spawn Settings")]
    [Tooltip("Total number of camps to place on the grid at game start")]
    public int totalCamps = 12;

    [Tooltip("Seconds before a cleared camp respawns")]
    public float respawnTimeSeconds = 300f;

    [Header("Resource Loot Ranges (min/max per level)")]
    public float foodLootMin = 20f;
    public float foodLootMax = 80f;
    public float woodLootMin = 20f;
    public float woodLootMax = 80f;
    public float stoneLootMin = 10f;
    public float stoneLootMax = 60f;
    public float silverLootMin = 5f;
    public float silverLootMax = 40f;

    [Header("Crafting Material Drop Chance (0-1 per camp level)")]
    [Tooltip("Base chance a crafting material drops. Multiplied by camp level.")]
    public float baseMaterialDropChance = 0.3f;

    // Returns RNG resource loot scaled by camp level
    public void RollResourceLoot(int campLevel, out float food, out float wood,
                                  out float stone, out float silver)
    {
        float scale = campLevel / 6f;
        food = Random.Range(foodLootMin, foodLootMax) * scale;
        wood = Random.Range(woodLootMin, woodLootMax) * scale;
        stone = Random.Range(stoneLootMin, stoneLootMax) * scale;
        silver = Random.Range(silverLootMin, silverLootMax) * scale;
    }

    // Returns a crafting material name if one drops, or null if not
    public string RollMaterialDrop(int campLevel)
    {
        float chance = baseMaterialDropChance * campLevel;
        if (Random.value > chance) return null;

        string[] materials = { "iron", "steel", "timber", "brass", "cotton", "leather" };
        return materials[Random.Range(0, materials.Length)];
    }
}
#endregion