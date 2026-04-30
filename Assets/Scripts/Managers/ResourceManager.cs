#region Summary
/// <summary>
/// ResourceManager is a singleton class responsible for managing the player's resources in the game, including food, wood, stone, and gold.
/// It keeps track of the current amounts of each resource, their storage capacities, and handles resource production based on the buildings constructed by the player.
/// Example usage:
/// - When a player collects resources, the ResourceManager can be used to add the collected resources to the player's inventory.
/// - When a player attempts to construct a building or upgrade a unit, the ResourceManager can be used to check if the player has sufficient resources and deduct the required amounts.
/// Note: The ResourceManager is a crucial part of the resource management system in the game and should be used consistently across all systems that involve resource handling to ensure clarity and maintainability of the codebase.
/// </summary>
#endregion
#region Phase 1 Sprint 4 - Resource System Implementation
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [Header("Current Resources")]
    public float food;
    public float wood;
    public float stone;
    public float gold;

    [Header("Storage Caps")]
    public float foodCap = 500f;
    public float woodCap = 500f;
    public float stoneCap = 500f;
    public float goldCap = 500f;

    [Header("Starting Resources")]
    [SerializeField] private float startFood = 300f;
    [SerializeField] private float startWood = 300f;
    [SerializeField] private float startStone = 300f;
    [SerializeField] private float startGold = 100f;

    private const float tickIntervalSeconds = 60f;
    private float tickTimer;

    public event Action OnResourceChanged;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        food = startFood;
        wood = startWood;
        stone = startStone;
        gold = startGold;

        OnResourceChanged?.Invoke();
    }

    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickIntervalSeconds)
        {
            tickTimer = 0f;
            RunProductionTick();
        }
    }

    void RunProductionTick()
    {
        if (BuildingManager.Instance == null) return;

        foreach (BuildingState b in BuildingManager.Instance.GetAllBuildings())
        {
            if (b.isUpgrading) continue;
            if (b.config.productionPerHour <= 0f) continue;

            float amountPerTick = b.config.productionPerHour / 60f;

            switch (b.config.buildingType)
            {
                case BuildingType.Farm: Add(ResourceType.Food, amountPerTick); break;
                case BuildingType.LumberMill: Add(ResourceType.Wood, amountPerTick); break;
                case BuildingType.Quarry: Add(ResourceType.Stone, amountPerTick); break;
                case BuildingType.GoldMine: Add(ResourceType.Gold, amountPerTick); break;
            }
        }
    }

    public void Add(ResourceType type, float amount)
    {
        switch (type)
        {
            case ResourceType.Food: food = Mathf.Min(food + amount, foodCap); break;
            case ResourceType.Wood: wood = Mathf.Min(wood + amount, woodCap); break;
            case ResourceType.Stone: stone = Mathf.Min(stone + amount, stoneCap); break;
            case ResourceType.Gold: gold = Mathf.Min(gold + amount, goldCap); break;
        }
        OnResourceChanged?.Invoke();
    }

    public bool CanAfford(ResourceCost cost)
    {
        return food >= cost.food && wood >= cost.wood
            && stone >= cost.stone && gold >= cost.gold;
    }

    public void Deduct(ResourceCost cost)
    {
        food -= cost.food;
        wood -= cost.wood;
        stone -= cost.stone;
        gold -= cost.gold;
        OnResourceChanged?.Invoke();
    }

    public void IncreaseStorageCap(float amount)
    {
        foodCap += amount;
        woodCap += amount;
        stoneCap += amount;
        goldCap += amount;
        OnResourceChanged?.Invoke();
    }
}
#endregion