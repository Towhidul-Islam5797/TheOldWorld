#region Phase 2 Sprint 6 - Gear Inventory
using UnityEngine;
using System;
using System.Collections.Generic;

public class GearInventory : MonoBehaviour
{
    public static GearInventory Instance;
    private Dictionary<string, int> items = new Dictionary<string, int>();
    public event Action OnInventoryChanged;

    void Awake()
    {
        Instance = this;
    }

    public void Add(string itemName, int amount)
    {
        if (!items.ContainsKey(itemName))
            items[itemName] = 0;
        items[itemName] += amount;
        OnInventoryChanged?.Invoke();
    }

    public int GetCount(string itemName)
    {
        return items.TryGetValue(itemName, out int count) ? count : 0;
    }
}
#endregion