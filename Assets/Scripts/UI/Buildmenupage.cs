#region Phase 2 Sprint 4 - Build Menu Page
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildMenuPage : MonoBehaviour
{
    [SerializeField] private BuildingConfig[] availableBuildings;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private int cardsPerPage = 6;
    [SerializeField] private TextMeshProUGUI pageIndicatorText;

    private int currentPage = 0;
    private int totalPages;

    public void Populate()
    {
        totalPages = Mathf.CeilToInt((float)availableBuildings.Length / cardsPerPage);
        currentPage = 0;
        ShowCurrentPage();
    }

    private void ShowCurrentPage()
    {
        int start = currentPage * cardsPerPage;

        for (int i = 0; i < cardContainer.childCount; i++)
        {
            GameObject card = cardContainer.GetChild(i).gameObject;
            int buildingIndex = start + i;

            if (buildingIndex < availableBuildings.Length)
            {
                BuildingConfig config = availableBuildings[buildingIndex];
                card.SetActive(true);

                // Use the card's own Image for the building sprite
                Image icon = card.GetComponent<Image>();
                if (icon != null && config.GetLevel(1).sprite != null)
                    icon.sprite = config.GetLevel(1).sprite;

                // Set name — find first TMP in children
                TextMeshProUGUI nameText = card.transform.Find("BuildingName")?.GetComponent<TextMeshProUGUI>();
                if (nameText != null)
                    nameText.text = config.buildingName;

                // Set cost — find CostText in children
                TextMeshProUGUI costText = card.transform.Find("CostText")?.GetComponent<TextMeshProUGUI>();
                if (costText != null)
                    costText.text = GetCostString(config.placementCost);

                // Wire button click
                Button btn = card.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.RemoveAllListeners();
                    BuildingConfig captured = config;
                    btn.onClick.AddListener(() => OnCardClicked(captured));
                }
            }
            else
            {
                card.SetActive(false);
            }
        }

        if (pageIndicatorText != null)
            pageIndicatorText.text = (currentPage + 1) + " / " + totalPages;
    }

    private string GetCostString(ResourceCost cost)
    {
        string result = "";
        if (cost.food > 0) result += "Food:" + cost.food + " ";
        if (cost.wood > 0) result += "Wood:" + cost.wood + " ";
        if (cost.stone > 0) result += "Stone:" + cost.stone + " ";
        if (cost.silver > 0) result += "Silver:" + cost.silver + " ";
        return result.Trim();
    }

    private void OnCardClicked(BuildingConfig config)
    {
        BuildingInteraction.Instance.Deselect();
        FindFirstObjectByType<BuildingPlacer>().SetBuilding(config);
    }

    public void OnLeftArrowClicked()
    {
        if (currentPage <= 0) return;
        currentPage--;
        ShowCurrentPage();
    }

    public void OnRightArrowClicked()
    {
        if (currentPage >= totalPages - 1) return;
        currentPage++;
        ShowCurrentPage();
    }
}
#endregion