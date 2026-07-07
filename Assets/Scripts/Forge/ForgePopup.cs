#region Phase 2 Sprint 6 - Forge Popup
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class ForgePopup : MonoBehaviour
{
    public static ForgePopup Instance;

    [Header("Popup Root")]
    [SerializeField] private GameObject popupRoot;

    [Header("Recipe Display")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI ingredientsText;

    [Header("Controls")]
    [SerializeField] private Button craftButton;

    private BuildingState sourceForge;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MaterialInventory.Instance.OnInventoryChanged += RefreshDisplay;
        Hide();
    }

    public void Show(BuildingState forge)
    {
        sourceForge = forge;
        popupRoot.SetActive(true);
        RefreshDisplay();
    }

    public void Hide()
    {
        popupRoot.SetActive(false);
    }

    public void OnCraftClicked()
    {
        CraftingRecipeConfig recipe = ForgeManager.Instance.SwordRecipe;
        bool crafted = ForgeManager.Instance.Craft(recipe);
        if (!crafted)
            Debug.Log("Not enough materials to craft " + recipe.itemName);
        RefreshDisplay();
    }

    public void OnBackClicked()
    {
        Hide();
        if (sourceForge != null)
            BuildingInteraction.Instance.SelectBuilding(sourceForge);
    }

    private void RefreshDisplay()
    {
        CraftingRecipeConfig recipe = ForgeManager.Instance.SwordRecipe;

        if (itemNameText != null)
            itemNameText.text = "Craft: " + recipe.itemName;

        if (ingredientsText != null)
        {
            StringBuilder builder = new StringBuilder();
            foreach (CraftingIngredient ingredient in recipe.ingredients)
            {
                int owned = MaterialInventory.Instance.GetCount(ingredient.materialName);
                builder.AppendLine(ingredient.materialName + ": " + owned + " / " + ingredient.amount);
            }
            ingredientsText.text = builder.ToString();
        }

        if (craftButton != null)
            craftButton.interactable = ForgeManager.Instance.CanCraft(recipe);
    }
}
#endregion