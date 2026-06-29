#region Phase 2 Sprint 3 - Building Popup UI
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingPopup : MonoBehaviour
{
    public static BuildingPopup Instance;

    [Header("Popup Root")]
    [SerializeField] private GameObject popupRoot;

    [Header("Info")]
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private Image levelBadgeImage;
    [SerializeField] private Sprite[] levelBadgeSprites;

    [Header("Special Button")]
    [SerializeField] private GameObject specialButton;
    [SerializeField] private TextMeshProUGUI specialButtonText;
    [SerializeField] private Sprite iconTrain;
    [SerializeField] private Sprite iconResearch;
    [SerializeField] private Sprite iconCraft;
    [SerializeField] private string textBarracks = "Train";
    [SerializeField] private string textAcademy = "Research";
    [SerializeField] private string textForge = "Craft";

    [Header("Upgrade / Cancel")]
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private GameObject cancelUpgradeButton;

    [Header("Position")]
    [SerializeField] private float popupYOffset = 120f;

    private BuildingState currentBuilding;
    private Vector3 anchorWorldPos;
    private RectTransform popupRectTransform;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        popupRectTransform = popupRoot.GetComponent<RectTransform>();
        Hide();
    }

    void Update()
    {
        if (currentBuilding == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(anchorWorldPos);
        popupRectTransform.position = screenPos + new Vector3(0, popupYOffset, 0);

        RefreshButtons();
    }

    public void Show(BuildingState building, Vector3 worldPos)
    {
        currentBuilding = building;
        anchorWorldPos = worldPos;

        buildingNameText.text = building.config.buildingName;

        int badgeIndex = Mathf.Clamp(building.level - 1, 0, levelBadgeSprites.Length - 1);
        levelBadgeImage.sprite = levelBadgeSprites[badgeIndex];

        RefreshButtons();
        popupRoot.SetActive(true);
    }

    public void Hide()
    {
        currentBuilding = null;
        popupRoot.SetActive(false);
    }

    private void RefreshButtons()
    {
        if (currentBuilding == null) return;

        bool isUpgrading = currentBuilding.isUpgrading;

        upgradeButton.SetActive(!isUpgrading);
        cancelUpgradeButton.SetActive(isUpgrading);

        Sprite specialIcon = GetSpecialIcon(currentBuilding.config.buildingType);
        string specialText = GetSpecialText(currentBuilding.config.buildingType);
        bool hasSpecial = specialIcon != null;

        specialButton.SetActive(hasSpecial && !isUpgrading);
        if (hasSpecial)
        {
            specialButton.GetComponent<Image>().sprite = specialIcon;
            if (specialButtonText != null)
                specialButtonText.text = specialText;
        }
    }

    private Sprite GetSpecialIcon(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.Barracks: return iconTrain;
            case BuildingType.Academy: return iconResearch;
            case BuildingType.Forge: return iconCraft;
            default: return null;
        }
    }

    private string GetSpecialText(BuildingType type)
    {
        switch (type)
        {
            case BuildingType.Barracks: return textBarracks;
            case BuildingType.Academy: return textAcademy;
            case BuildingType.Forge: return textForge;
            default: return "";
        }
    }

    public void OnUpgradeClicked()
    {
        if (currentBuilding == null) return;
        BuildingManager.Instance.UpgradeBuilding(currentBuilding.tileX, currentBuilding.tileY);
        RefreshButtons();
    }

    public void OnCancelUpgradeClicked()
    {
        if (currentBuilding == null) return;
        BuildingManager.Instance.CancelUpgrade(currentBuilding.tileX, currentBuilding.tileY);
        RefreshButtons();
    }

    public void OnInfoClicked()
    {
        if (currentBuilding == null) return;
        Debug.Log("Info: " + currentBuilding.config.buildingName + " Level " + currentBuilding.level);
    }

    public void OnSpecialClicked()
    {
        if (currentBuilding == null) return;
        switch (currentBuilding.config.buildingType)
        {
            case BuildingType.Barracks:
                Debug.Log("Open Train panel");
                break;
            case BuildingType.Academy:
                Debug.Log("Open Research panel");
                break;
            case BuildingType.Forge:
                Debug.Log("Open Craft panel");
                break;
        }
    }
}
#endregion