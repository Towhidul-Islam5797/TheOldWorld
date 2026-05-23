#region Summary
/// <summary>
/// TroopInventory is a singleton class that manages the player's inventory of troops in the game.
/// It keeps track of the quantity of each type of troop and provides methods to add troops and retrieve their counts.
/// The class also includes an event, OnInventoryChanged, which is invoked whenever the inventory is updated, allowing other parts of the game to react to changes in the troop inventory.
/// Example usage:
/// 1. When a player completes a training job, the Add method can be called to update the inventory with the newly trained troops.
/// 2. Other game systems, such as the UI or battle mechanics, can subscribe to the OnInventoryChanged event to update displays or recalculate battle outcomes based on the current troop counts.
/// Note: The TroopInventory class relies on the TroopType enum to define the different types of troops available in the game. This allows for easy expansion and management of various troop types as the game evolves.
/// </summary>
#endregion
#region Phase 1 Sprint 5 - Troop Inventory Class
//using UnityEngine;
//using System;
//using System.Collections.Generic;

//public class TroopInventory : MonoBehaviour
//{
//    public static TroopInventory Instance;

//    private Dictionary<TroopType, int> troops = new Dictionary<TroopType, int>();

//    public event Action OnInventoryChanged;

//    void Awake()
//    {
//        Instance = this;

//        foreach (TroopType type in Enum.GetValues(typeof(TroopType)))
//            troops[type] = 0;
//    }

//    public void Add(TroopType type, int amount)
//    {
//        troops[type] += amount;
//        Debug.Log(amount + " " + type + " added. Total: " + troops[type]);
//        OnInventoryChanged?.Invoke();
//    }

//    public int GetCount(TroopType type)
//    {
//        return troops[type];
//    }
//}
#endregion

#region Phase 1 Sprint 10 - Troop Inventory Class
using UnityEngine;
using System;
using System.Collections.Generic;
 
public class TroopInventory : MonoBehaviour
{
    public static TroopInventory Instance;

    private Dictionary<TroopType, int> troops = new Dictionary<TroopType, int>();

    public event Action OnInventoryChanged;

    void Awake()
    {
        Instance = this;

        foreach (TroopType type in Enum.GetValues(typeof(TroopType)))
            troops[type] = 0;
    }

    public void Add(TroopType type, int amount)
    {
        troops[type] += amount;
        Debug.Log(amount + " " + type + " added. Total: " + troops[type]);
        OnInventoryChanged?.Invoke();
    }

    public void Deduct(TroopType type, int amount)
    {
        troops[type] = Mathf.Max(0, troops[type] - amount);
        OnInventoryChanged?.Invoke();
    }

    public int GetCount(TroopType type)
    {
        return troops[type];
    }
}
#endregion