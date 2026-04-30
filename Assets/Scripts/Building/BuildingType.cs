#region Summary
/// <summary>
/// BuildingType is an enumeration that defines the different types of buildings available in the game.
/// Each type represents a specific category of building with unique functions and characteristics.
/// This enum is used throughout the codebase to identify and manage building types, allowing for organized handling of building-related logic and interactions.
/// Example usage:
/// 1. When creating a new BuildingConfig, you can set the buildingType to one of the values defined in this enum (e.g., BuildingType.Farm).
/// 2. In the BuildingState class, you can check the buildingType to determine specific behaviors or restrictions based on the type of building (e.g., only allowing certain upgrades for HQ buildings).
/// 3. When rendering the building on the map, you can use the buildingType to determine which sprite or model to display for that building.
/// Note: The specific building types included in this enum (HQ, Barracks, Farm, etc.) are examples and can be expanded or modified as needed to fit the game's design and requirements.
/// </summary>
#endregion
#region Phase 1 Sprint 3 - Building Type Enumeration
public enum BuildingType
{
    HQ,
    Barracks,
    Farm,
    LumberMill,
    Quarry,
    GoldMine,
    Storehouse,
    Walls,
    Watchtower
}
#endregion