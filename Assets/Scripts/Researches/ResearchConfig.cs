#region Phase 2 Sprint 7 - Research Config
using UnityEngine;

[CreateAssetMenu(fileName = "ResearchConfig", menuName = "TheOldWorld/ResearchConfig")]
public class ResearchConfig : ScriptableObject
{
    public string researchName;
    public int foodCost;
    public int woodCost;
    public int stoneCost;
    public int researchTimeSeconds;
    public float foodProductionBoost;
}
#endregion