#region Summary
/// <summary>
/// TroopType is an enumeration that defines the different types of troops available in the game.
/// Each type represents a specific category of troop with unique combat roles and characteristics.
/// This enum is used throughout the codebase to identify and manage troop types, allowing for organized handling of troop-related logic and interactions.
/// Example usage:
/// 1. When creating a new TroopConfig, you can set the troopType to one of the values defined in this enum (e.g., TroopType.Infantry).
/// 2. In the TroopState class, you can check the troopType to determine specific behaviors or combat strategies based on the type of troop (e.g., Cavalry may have higher movement speed but lower defense).
/// 3. When rendering the troop on the map, you can use the troopType to determine which sprite or model to display for that troop.
/// Note: The specific troop types included in this enum (Infantry, Archers, Cavalry, HorseArchers) are examples and can be expanded or modified as needed to fit the game's design and requirements.
/// </summary>
#endregion
#region Phase 1 Sprint 5 - Troop Type Enumeration
public enum TroopType
{
    Infantry,
    Archers,
    Cavalry,
    HorseArchers
}
#endregion