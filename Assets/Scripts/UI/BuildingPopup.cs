#region Phase 2 Sprint 3 - Building Popup UI
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class BuildingPopup : MonoBehaviour
//{
//    public static BuildingPopup Instance;

//    [Header("Popup Root")]
//    [SerializeField] private GameObject popupRoot;

//    [Header("Info")]
//    [SerializeField] private TextMeshProUGUI buildingNameText;
//    [SerializeField] private Image levelBadgeImage;
//    [SerializeField] private Sprite[] levelBadgeSprites;

//    [Header("Special Button")]
//    [SerializeField] private GameObject specialButton;
//    [SerializeField] private TextMeshProUGUI specialButtonText;
//    [SerializeField] private Sprite iconTrain;
//    [SerializeField] private Sprite iconResearch;
//    [SerializeField] private Sprite iconCraft;
//    [SerializeField] private string textBarracks = "Train";
//    [SerializeField] private string textAcademy = "Research";
//    [SerializeField] private string textForge = "Craft";

//    [Header("Upgrade / Cancel")]
//    [SerializeField] private GameObject upgradeButton;
//    [SerializeField] private GameObject cancelUpgradeButton;

//    [Header("Position")]
//    [SerializeField] private float popupYOffset = 120f;

//    private BuildingState currentBuilding;
//    private Vector3 anchorWorldPos;
//    private RectTransform popupRectTransform;

//    void Awake()
//    {
//        Instance = this;
//    }

//    void Start()
//    {
//        popupRectTransform = popupRoot.GetComponent<RectTransform>();
//        Hide();
//    }

//    void Update()
//    {
//        if (currentBuilding == null) return;

//        Vector3 screenPos = Camera.main.WorldToScreenPoint(anchorWorldPos);
//        popupRectTransform.position = screenPos + new Vector3(0, popupYOffset, 0);

//        RefreshButtons();
//    }

//    public void Show(BuildingState building, Vector3 worldPos)
//    {
//        currentBuilding = building;
//        anchorWorldPos = worldPos;

//        buildingNameText.text = building.config.buildingName;

//        int badgeIndex = Mathf.Clamp(building.level - 1, 0, levelBadgeSprites.Length - 1);
//        levelBadgeImage.sprite = levelBadgeSprites[badgeIndex];

//        RefreshButtons();
//        popupRoot.SetActive(true);
//    }

//    public void Hide()
//    {
//        currentBuilding = null;
//        popupRoot.SetActive(false);
//    }

//    private void RefreshButtons()
//    {
//        if (currentBuilding == null) return;

//        bool isUpgrading = currentBuilding.isUpgrading;

//        upgradeButton.SetActive(!isUpgrading);
//        cancelUpgradeButton.SetActive(isUpgrading);

//        Sprite specialIcon = GetSpecialIcon(currentBuilding.config.buildingType);
//        string specialText = GetSpecialText(currentBuilding.config.buildingType);
//        bool hasSpecial = specialIcon != null;

//        specialButton.SetActive(hasSpecial && !isUpgrading);
//        if (hasSpecial)
//        {
//            specialButton.GetComponent<Image>().sprite = specialIcon;
//            if (specialButtonText != null)
//                specialButtonText.text = specialText;
//        }
//    }

//    private Sprite GetSpecialIcon(BuildingType type)
//    {
//        switch (type)
//        {
//            case BuildingType.Barracks: return iconTrain;
//            case BuildingType.Academy: return iconResearch;
//            case BuildingType.Forge: return iconCraft;
//            default: return null;
//        }
//    }

//    private string GetSpecialText(BuildingType type)
//    {
//        switch (type)
//        {
//            case BuildingType.Barracks: return textBarracks;
//            case BuildingType.Academy: return textAcademy;
//            case BuildingType.Forge: return textForge;
//            default: return "";
//        }
//    }

//    public void OnUpgradeClicked()
//    {
//        if (currentBuilding == null) return;
//        BuildingManager.Instance.UpgradeBuilding(currentBuilding.tileX, currentBuilding.tileY);
//        RefreshButtons();
//    }

//    public void OnCancelUpgradeClicked()
//    {
//        if (currentBuilding == null) return;
//        BuildingManager.Instance.CancelUpgrade(currentBuilding.tileX, currentBuilding.tileY);
//        RefreshButtons();
//    }

//    public void OnInfoClicked()
//    {
//        if (currentBuilding == null) return;
//        Debug.Log("Info: " + currentBuilding.config.buildingName + " Level " + currentBuilding.level);
//    }

//    public void OnSpecialClicked()
//    {
//        if (currentBuilding == null) return;
//        switch (currentBuilding.config.buildingType)
//        {
//            case BuildingType.Barracks:
//                Debug.Log("Open Train panel");
//                break;
//            case BuildingType.Academy:
//                Debug.Log("Open Research panel");
//                break;
//            case BuildingType.Forge:
//                Debug.Log("Open Craft panel");
//                break;
//        }
//    }
//}
#endregion

#region Phase 2 Sprint 4 - Building Popup With Build Menu Page
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingPopup : MonoBehaviour
{
    public static BuildingPopup Instance;

    [Header("Popup Root")]
    [SerializeField] private GameObject popupRoot;

    [Header("Page 1 - Actions")]
    [SerializeField] private GameObject page1;

    [Header("Info")]
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private Image levelBadgeImage;
    [SerializeField] private Sprite[] levelBadgeSprites;

    [Header("Special Button")]
    [SerializeField] private GameObject specialButton;
    [SerializeField] private TextMeshProUGUI specialButtonText;
    [SerializeField] private Sprite iconBuild;
    [SerializeField] private Sprite iconTrain;
    [SerializeField] private Sprite iconResearch;
    [SerializeField] private Sprite iconCraft;
    [SerializeField] private string textHQ = "Build";
    [SerializeField] private string textBarracks = "Train";
    [SerializeField] private string textAcademy = "Research";
    [SerializeField] private string textForge = "Craft";

    [Header("Upgrade / Cancel")]
    [SerializeField] private GameObject upgradeButton;
    [SerializeField] private GameObject cancelUpgradeButton;

    [Header("Page 2 - Build Menu")]
    [SerializeField] private GameObject page2;
    [SerializeField] private BuildMenuPage buildMenuPage;
    [SerializeField] private Image page2LevelBadgeImage;

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
        Sprite badgeSprite = levelBadgeSprites[badgeIndex];
        levelBadgeImage.sprite = badgeSprite;
        if (page2LevelBadgeImage != null)
            page2LevelBadgeImage.sprite = badgeSprite;

        ShowPage1();
        popupRoot.SetActive(true);
    }

    public void Hide()
    {
        currentBuilding = null;
        popupRoot.SetActive(false);
    }

    private void ShowPage1()
    {
        page1.SetActive(true);
        page2.SetActive(false);
        RefreshButtons();
    }

    private void ShowPage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
        buildMenuPage.Populate();
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
            case BuildingType.HQ: return iconBuild;
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
            case BuildingType.HQ: return textHQ;
            case BuildingType.Barracks: return textBarracks;
            case BuildingType.Academy: return textAcademy;
            case BuildingType.Forge: return textForge;
            default: return "";
        }
    }

    // Wired via Inspector OnClick
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
            case BuildingType.HQ:
                ShowPage2();
                break;
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

    // Page 2 dedicated Back button — wired via Inspector OnClick
    public void OnBackClicked()
    {
        ShowPage1();
    }
}
#endregion