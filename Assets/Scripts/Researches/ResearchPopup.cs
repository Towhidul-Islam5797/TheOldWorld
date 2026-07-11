#region Summary
/// <summary>
/// This class represents the configuration for a building in the game. It contains information about the building's name, type, maximum level, costs for upgrading and placement, production rate, 
/// visual representation, footprint size, and level-specific data.
/// Usage:
///  1. Create a new BuildingConfig asset in the Unity Editor.
///  2. Set the buildingName, buildingType, maxLevel, upgradeTimeSeconds, upgradeCost, placementCost, productionPerHour, buildingSprite, footprintWidth, footprintHeight, and levelConfigs as needed.
///  3. Use this BuildingConfig when creating a new BuildingState for a building in the game.
///  4. This design promotes separation of data and logic, making the codebase more maintainable and flexible.
/// Note: BuildingType is an enum that should be defined elsewhere in the codebase, representing different categories of buildings (e.g., HQ, Farm, Barracks).
/// </summary>
#endregion

#region Phase 2 Sprint 7 - Research Popup
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResearchPopup : MonoBehaviour
{
    public static ResearchPopup Instance;

    [Header("Popup Root")]
    [SerializeField] private GameObject popupRoot;

    [Header("Research Display")]
    [SerializeField] private TextMeshProUGUI researchNameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Controls")]
    [SerializeField] private Button researchButton;

    private BuildingState sourceAcademy;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Hide();
    }

    void Update()
    {
        if (popupRoot.activeSelf)
            RefreshDisplay();
    }

    public void Show(BuildingState academy)
    {
        sourceAcademy = academy;
        popupRoot.SetActive(true);
        RefreshDisplay();
    }

    public void Hide()
    {
        popupRoot.SetActive(false);
    }

    public void OnResearchClicked()
    {
        ResearchManager.Instance.StartResearch(ResearchManager.Instance.CurrentResearch);
        RefreshDisplay();
    }

    public void OnBackClicked()
    {
        Hide();
        if (sourceAcademy != null)
            BuildingInteraction.Instance.SelectBuilding(sourceAcademy);
    }

    private void RefreshDisplay()
    {
        ResearchConfig research = ResearchManager.Instance.CurrentResearch;

        if (researchNameText != null)
            researchNameText.text = research.researchName;

        if (costText != null)
            costText.text = "Food: " + research.foodCost +
                             "  Wood: " + research.woodCost +
                             "  Stone: " + research.stoneCost;

        bool isResearching = ResearchManager.Instance.IsResearching;

        if (timerText != null)
            timerText.text = isResearching
                ? "Researching: " + Mathf.CeilToInt(ResearchManager.Instance.GetRemainingSeconds()) + "s"
                : "";

        if (researchButton != null)
            researchButton.interactable = !isResearching && ResearchManager.Instance.CanAfford(research);
    }
}
#endregion