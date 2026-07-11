#region Phase 2 Sprint 6 - Material Inventory
using UnityEngine;
using System;
using System.Collections.Generic;

public class MaterialInventory : MonoBehaviour
{
    public static MaterialInventory Instance;
    private Dictionary<string, int> materials = new Dictionary<string, int>();
    public event Action OnInventoryChanged;

    void Awake()
    {
        Instance = this;
    }

    public void Add(string materialName, int amount)
    {
        if (!materials.ContainsKey(materialName))
            materials[materialName] = 0;
        materials[materialName] += amount;
        OnInventoryChanged?.Invoke();
    }

    public void Deduct(string materialName, int amount)
    {
        if (!materials.ContainsKey(materialName)) return;
        materials[materialName] = Mathf.Max(0, materials[materialName] - amount);
        OnInventoryChanged?.Invoke();
    }

    public int GetCount(string materialName)
    {
        return materials.TryGetValue(materialName, out int count) ? count : 0;
    }
}
#endregion