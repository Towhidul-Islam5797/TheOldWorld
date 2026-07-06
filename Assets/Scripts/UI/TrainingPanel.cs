#region Phase 2 Sprint 5 - Training Panel
#region Phase 2 Sprint 5 - Training Panel
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrainingPanel : MonoBehaviour
{
    [Header("Troop Data")]
    [SerializeField] private TroopConfig[] availableTroops;

    [Header("Card Container")]
    [SerializeField] private Transform cardContainer;

    [Header("Info Panel")]
    [SerializeField] private Image troopIcon;
    [SerializeField] private TextMeshProUGUI troopName;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI timeText;

    [Header("Training Controls")]
    [SerializeField] private Button minusButton;
    [SerializeField] private Button plusButton;
    [SerializeField] private Button trainButton;
    [SerializeField] private TextMeshProUGUI quantityText;

    [Header("Queue Panel")]
    [SerializeField] private GameObject queueSlot1;
    [SerializeField] private Image slot1Icon;
    [SerializeField] private TextMeshProUGUI slot1Label;
    [SerializeField] private TextMeshProUGUI slot1Timer;
    [SerializeField] private GameObject queueSlot2;
    [SerializeField] private Image slot2Icon;
    [SerializeField] private TextMeshProUGUI slot2Label;
    [SerializeField] private TextMeshProUGUI slot2Timer;

    [Header("Level Badge Sprites")]
    [SerializeField] private Sprite[] levelBadgeSprites;

    private TroopConfig selectedTroop;
    private int quantity = 1;

    void Start()
    {
        SetupCards();
        minusButton.onClick.AddListener(OnMinusClicked);
        plusButton.onClick.AddListener(OnPlusClicked);
        trainButton.onClick.AddListener(OnTrainClicked);
        ClearInfoPanel();
    }

    void OnEnable()
    {
        RefreshCards();
        ClearInfoPanel();
        quantity = 1;
        UpdateQuantityText();
    }

    void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        RefreshQueueDisplay();
    }

    private void SetupCards()
    {
        for (int i = 0; i < cardContainer.childCount; i++)
        {
            if (i >= availableTroops.Length) break;

            GameObject card = cardContainer.GetChild(i).gameObject;
            TroopConfig config = availableTroops[i];

            Image icon = card.GetComponent<Image>();
            if (icon != null && config.GetLevel(1).icon != null)
                icon.sprite = config.GetLevel(1).icon;

            TextMeshProUGUI nameText = card.transform.Find("CardName")?.GetComponent<TextMeshProUGUI>();
            if (nameText != null) nameText.text = config.troopName;

            Image levelBadge = card.transform.Find("LevelBadge")?.GetComponent<Image>();
            if (levelBadge != null && levelBadgeSprites.Length > 0)
                levelBadge.sprite = levelBadgeSprites[0];

            Button btn = card.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.RemoveAllListeners();
                TroopConfig captured = config;
                btn.onClick.AddListener(() => SelectTroop(captured));
            }
        }
    }

    private void RefreshCards()
    {
        int barracksLevel = GetBarracksLevel();

        for (int i = 0; i < cardContainer.childCount; i++)
        {
            if (i >= availableTroops.Length) break;

            GameObject card = cardContainer.GetChild(i).gameObject;
            TroopConfig config = availableTroops[i];

            GameObject lockedOverlay = card.transform.Find("LockedOverlay")?.gameObject;
            if (lockedOverlay != null)
                lockedOverlay.SetActive(barracksLevel < config.requiredBarracksLevel);
        }
    }

    private void SelectTroop(TroopConfig config)
    {
        selectedTroop = config;
        quantity = 1;
        UpdateQuantityText();
        UpdateInfoPanel();
    }

    private void UpdateInfoPanel()
    {
        if (selectedTroop == null) return;

        TroopLevelData level1 = selectedTroop.GetLevel(1);

        if (troopIcon != null && level1.icon != null)
            troopIcon.sprite = level1.icon;

        if (troopName != null)
            troopName.text = selectedTroop.troopName;

        if (statsText != null)
            statsText.text = "ATK: " + level1.baseAttack + "  DEF: " + level1.baseDefense + "  HP: " + level1.baseHealth;

        if (costText != null)
            costText.text = GetCostString(selectedTroop.trainingCostPerUnit);

        if (timeText != null)
            timeText.text = selectedTroop.trainingTimeSecondsPerUnit + "s per unit";
    }

    private void ClearInfoPanel()
    {
        selectedTroop = null;
        if (troopName != null) troopName.text = "Select a troop";
        if (statsText != null) statsText.text = "";
        if (costText != null) costText.text = "";
        if (timeText != null) timeText.text = "";
        if (troopIcon != null) troopIcon.sprite = null;
    }

    private void OnMinusClicked()
    {
        quantity = Mathf.Max(1, quantity - 1);
        UpdateQuantityText();
    }

    private void OnPlusClicked()
    {
        quantity++;
        UpdateQuantityText();
    }

    private void UpdateQuantityText()
    {
        if (quantityText != null)
            quantityText.text = quantity.ToString();
    }

    private void OnTrainClicked()
    {
        if (selectedTroop == null)
        {
            Debug.Log("No troop selected.");
            return;
        }

        bool started = TrainingManager.Instance.StartTraining(selectedTroop, quantity);
        if (started)
        {
            quantity = 1;
            UpdateQuantityText();
        }
        else
        {
            Debug.Log("Training failed. Queue full or not enough resources.");
        }
    }

    private void RefreshQueueDisplay()
    {
        TrainingJob[] jobs = TrainingManager.Instance.GetQueueSnapshot();

        UpdateQueueSlot(0, jobs, queueSlot1, slot1Icon, slot1Label, slot1Timer);
        UpdateQueueSlot(1, jobs, queueSlot2, slot2Icon, slot2Label, slot2Timer);
    }

    private void UpdateQueueSlot(int index, TrainingJob[] jobs, GameObject root, Image icon, TextMeshProUGUI label, TextMeshProUGUI timer)
    {
        if (index < jobs.Length)
        {
            root.SetActive(true);
            TrainingJob job = jobs[index];

            if (icon != null && job.config.GetLevel(1).icon != null)
                icon.sprite = job.config.GetLevel(1).icon;

            if (label != null)
                label.text = job.quantity + "x " + job.config.troopName;

            if (timer != null)
            {
                TimeSpan remaining = job.completionTime - DateTime.UtcNow;
                if (remaining.TotalSeconds < 0) remaining = TimeSpan.Zero;
                timer.text = string.Format("{0:00}:{1:00}", (int)remaining.TotalMinutes, remaining.Seconds);
            }
        }
        else
        {
            root.SetActive(false);
        }
    }

    private int GetBarracksLevel()
    {
        foreach (BuildingState b in BuildingManager.Instance.GetAllBuildings())
            if (b.config.buildingType == BuildingType.Barracks) return b.level;
        return 0;
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
}
#endregion
#endregion