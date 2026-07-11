#region Summary
/// <summary>
/// TroopConfig is a ScriptableObject that defines the configuration for a specific type of troop in the game.
/// It includes properties such as the troop's name, type, required building for training, attack and defense values, training cost, and training time.
/// This configuration allows for easy management and customization of different troop types within the game.
/// Example usage:
/// 1. When creating a new TroopConfig, you can set the troopType to one of the values defined in the TroopType enum (e.g., TroopType.Infantry).
/// 2. In the TroopState class, you can check the troopType to determine specific behaviors or combat strategies based on the type of troop (e.g., Cavalry may have higher movement speed but lower defense).
/// 3. When rendering the troop on the map, you can use the troopType to determine which sprite or model to display for that troop.
/// Note: The specific troop types included in the TroopType enum (Infantry, Archers, Cavalry, HorseArchers) are examples and can be expanded or modified as needed to fit the game's design and requirements.
/// </summary>
#endregion
#region Phase 1 Sprint 5 - Troop Configuration ScriptableObject
//using UnityEngine;

//[CreateAssetMenu(fileName = "TroopConfig", menuName = "TheOldWorld/TroopConfig")]
//public class TroopConfig : ScriptableObject
//{
//    public string troopName;
//    public TroopType troopType;
//    public BuildingType requiredBuilding;
//    public int attack;
//    public int defense;
//    public ResourceCost trainingCostPerUnit;
//    public float trainingTimeSecondsPerUnit;
//}
#endregion
#region Phase 1 Sprint 8 - Troop Configuration ScriptableObject Updated
//using UnityEngine;

//[CreateAssetMenu(fileName = "TroopConfig", menuName = "TheOldWorld/TroopConfig")]
//public class TroopConfig : ScriptableObject
//{
//    public string troopName;
//    public TroopType troopType;
//    public BuildingType requiredBuilding;
//    public int baseattack;
//    public int basedefense;
//    public int baseHealth;
//    public ResourceCost trainingCostPerUnit;
//    public float trainingTimeSecondsPerUnit;
//}
#endregion
#region Client Revision - Barracks Level Gating
//using UnityEngine;

//[CreateAssetMenu(fileName = "TroopConfig", menuName = "TheOldWorld/TroopConfig")]
//public class TroopConfig : ScriptableObject
//{
//    public string troopName;
//    public TroopType troopType;
//    [Tooltip("Minimum Barracks level required to train this troop. Infantry=1, Archers=2, Cavalry=3, Siege=4")]
//    public int requiredBarracksLevel;
//    public int baseAttack;
//    public int baseDefense;
//    public int baseHealth;
//    public ResourceCost trainingCostPerUnit;
//    public float trainingTimeSecondsPerUnit;
//}
#endregion

#region Phase 2 Sprint 5 - Troop Configuration With Per-Level Data
using UnityEngine;

[System.Serializable]
public class TroopLevelData
{
    public Sprite icon;
    public int baseAttack;
    public int baseDefense;
    public int baseHealth;
}

[CreateAssetMenu(fileName = "TroopConfig", menuName = "TheOldWorld/TroopConfig")]
public class TroopConfig : ScriptableObject
{
    [Header("Identity")]
    public string troopName;
    public TroopType troopType;

    [Header("Unlock")]
    [Tooltip("Minimum Barracks level required to train this troop at Level 1. Infantry=1, Archers=2, Cavalry=3, Siege=4")]
    public int requiredBarracksLevel;

    [Header("Training (Level 1 only)")]
    public ResourceCost trainingCostPerUnit;
    public float trainingTimeSecondsPerUnit;

    [Header("Levels")]
    public TroopLevelData[] levels;

    public int maxLevel => levels.Length;

    public TroopLevelData GetLevel(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, levels.Length - 1);
        return levels[index];
    }
}
#endregion