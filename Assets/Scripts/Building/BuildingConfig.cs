#region Summary
/// <summary>
/// BuildingConfig is a ScriptableObject that holds configuration data for different types of buildings in the game.
/// It includes the building's name, type, maximum level, and the time required to upgrade to the next level.
/// This allows for easy creation and management of building types through the Unity Editor, enabling designers to tweak building parameters without modifying code.
/// Example usage:
/// 1. Create a new BuildingConfig asset in the Unity Editor.
/// 2. Set the buildingName to "Farm", buildingType to BuildingType.Farm, maxLevel to 5, and upgradeTimeSeconds to 60.
/// 3. Use this BuildingConfig when creating a new BuildingState for a farm building in the game.
/// This design promotes separation of data and logic, making the codebase more maintainable and flexible.
/// Note: BuildingType is an enum that should be defined elsewhere in the codebase, representing different categories of buildings (e.g., HQ, Farm, Barracks).
/// </summary>
#endregion
#region Phase 1 Sprint 3 - Building Configuration
//using UnityEngine;

//[CreateAssetMenu(fileName = "BuildingConfig", menuName = "TheOldWorld/BuildingConfig")]
//public class BuildingConfig : ScriptableObject
//{
//    public string buildingName;
//    public BuildingType buildingType;
//    public int maxLevel;
//    public float upgradeTimeSeconds;
//}
#endregion
#region Phase 2 Sprint 4 - Building Configuration Extended
//using UnityEngine;

//[CreateAssetMenu(fileName = "BuildingConfig", menuName = "TheOldWorld/BuildingConfig")]
//public class BuildingConfig : ScriptableObject
//{
//    public string buildingName;
//    public BuildingType buildingType;
//    public int maxLevel;
//    public float upgradeTimeSeconds;
//    public ResourceCost upgradeCost;
//    public ResourceCost placementCost;
//    public float productionPerHour;
//}
#endregion
#region Phase 1 Sprint 7 - Building Configuration Extended
//using UnityEngine;

//[CreateAssetMenu(fileName = "BuildingConfig", menuName = "TheOldWorld/BuildingConfig")]
//public class BuildingConfig : ScriptableObject
//{
//    public string buildingName;
//    public BuildingType buildingType;
//    public int maxLevel;
//    public float upgradeTimeSeconds;
//    public ResourceCost upgradeCost;
//    public ResourceCost placementCost;
//    public float productionPerHour;

//    [Header("Visuals")]
//    public Sprite buildingSprite;

//    [Header("Footprint")]
//    public int footprintWidth = 1;
//    public int footprintHeight = 1;
//}
#endregion

#region Phase 2 Sprint 1 - Building Configuration With Levels
using UnityEngine;

[System.Serializable]
public class BuildingLevelData
{
    public Sprite sprite;
    public float upgradeTimeSeconds;
    public ResourceCost upgradeCost;
    public float productionPerHour;
}

[CreateAssetMenu(fileName = "BuildingConfig", menuName = "TheOldWorld/BuildingConfig")]
public class BuildingConfig : ScriptableObject
{
    [Header("Identity")]
    public string buildingName;
    public BuildingType buildingType;

    [Header("Placement")]
    public ResourceCost placementCost;
    public int footprintWidth = 1;
    public int footprintHeight = 1;

    [Header("Levels")]
    public BuildingLevelData[] levels;

    public int maxLevel => levels.Length;

    public BuildingLevelData GetLevel(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, levels.Length - 1);
        return levels[index];
    }
}
#endregion