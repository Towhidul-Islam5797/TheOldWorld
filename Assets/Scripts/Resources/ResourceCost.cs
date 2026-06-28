#region Summary
/// <summary>
/// ResourceCost is a class that represents the cost of resources required for various actions in the game, such as building construction, unit training, or upgrades.
/// This class contains fields for each type of resource (food, wood, stone, and gold) and can be used to define the specific amounts of each resource needed for a particular action.
/// Example usage:
/// - When a player attempts to construct a building, an instance of ResourceCost can be created to specify the required amounts of food, wood, stone, and gold for that building.
/// - When a player tries to train a unit, an instance of ResourceCost can be used to determine the resource requirements for that unit and 
///     check if the player has sufficient resources before allowing the training to proceed.
/// Note: The ResourceCost class is a crucial part of the resource management system in the game and should be used consistently across 
///     all systems that involve resource requirements to ensure clarity and maintainability of the codebase.
/// </summary>
#endregion
#region Phase 1 Sprint 4 - Resource System Implementation
//[System.Serializable]
//public class ResourceCost
//{
//    public float food;
//    public float wood;
//    public float stone;
//    public float gold;
//}
#endregion 

#region Phase 1 Sprint 9 - Resource System Refactored Gold Renamed to Silver
//[System.Serializable]
//public class ResourceCost
//{
//    public float food;
//    public float wood;
//    public float stone;
//    public float silver;
//}
#endregion

#region Phase 2 Sprint 2 - Gold Added as Premium Currency
[System.Serializable]
public class ResourceCost
{
    public float food;
    public float wood;
    public float stone;
    public float silver;
    public float gold;
}
#endregion