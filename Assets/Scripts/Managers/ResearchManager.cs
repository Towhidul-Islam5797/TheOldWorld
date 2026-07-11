#region Summary
/// <summary>
/// The ResearchManager class is responsible for managing the research system in the game. It handles starting and completing research, checking if the player can afford research, and applying research effects.
/// </summary>
#endregion
#region Phase 2 Sprint 7 - Research Manager
using UnityEngine;
using System;

public class ResearchManager : MonoBehaviour
{
    public static ResearchManager Instance;

    [SerializeField] private ResearchConfig currentResearch;
    public ResearchConfig CurrentResearch => currentResearch;

    private bool isResearching;
    public bool IsResearching => isResearching;

    private DateTime completionTime;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!isResearching) return;

        if (DateTime.UtcNow >= completionTime)
        {
            CompleteResearch();
        }
    }

    public bool CanAfford(ResearchConfig research)
    {
        ResourceCost cost = BuildCost(research);
        return ResourceManager.Instance.CanAfford(cost);
    }

    public void StartResearch(ResearchConfig research)
    {
        if (isResearching) return;
        if (!CanAfford(research)) return;

        ResourceManager.Instance.Deduct(BuildCost(research));
        completionTime = DateTime.UtcNow.AddSeconds(research.researchTimeSeconds);
        isResearching = true;
    }

    public float GetRemainingSeconds()
    {
        if (!isResearching) return 0f;
        double seconds = (completionTime - DateTime.UtcNow).TotalSeconds;
        return Mathf.Max(0f, (float)seconds);
    }

    private void CompleteResearch()
    {
        isResearching = false;
        ResourceManager.Instance.foodProductionMultiplier += currentResearch.foodProductionBoost;
        Debug.Log(currentResearch.researchName + " research complete. Food production boosted.");
    }

    private ResourceCost BuildCost(ResearchConfig research)
    {
        ResourceCost cost = new ResourceCost();
        cost.food = research.foodCost;
        cost.wood = research.woodCost;
        cost.stone = research.stoneCost;
        return cost;
    }
}
#endregion