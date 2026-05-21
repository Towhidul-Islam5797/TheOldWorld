#region Summary
/// <summary>
/// ResourceType is an enumeration that defines the different types of resources available in the game.
/// This enum is used to categorize and manage resources such as Food, Wood, Stone, and Gold, which are essential for various gameplay mechanics including building construction, upgrades, and unit training.
/// Each resource type can be associated with specific buildings, units, or actions that require that resource for production or maintenance.
/// Example usage:
/// - When a player collects resources, the ResourceType enum can be used to specify which type of resource is being collected and how it should be added to the player's inventory.
/// - When a player attempts to construct a building or upgrade a unit, the ResourceType enum can be used to determine which resources are required and to check if the player
///     has sufficient amounts of those resources before allowing the action to proceed.
/// Note: The ResourceType enum is a fundamental part of the resource management system in the game and should be used consistently across all systems that involve 
///     resource handling to ensure clarity and maintainability of the codebase.
/// </summary>
#endregion
#region Phase 1 Sprint 4 - Resource System Implementation
//public enum ResourceType
//{
//    Food,
//    Wood,
//    Stone,
//    Gold
//}
#endregion
#region Phase 1 Sprint 9 - Resource System Refactored Gold Renamed to Silver
public enum ResourceType
{
    Food,
    Wood,
    Stone,
    Silver //Gold
}
#endregion