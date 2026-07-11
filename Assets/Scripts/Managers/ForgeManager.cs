#region Phase 2 Sprint 6 - Forge Manager
using UnityEngine;

public class ForgeManager : MonoBehaviour
{
    public static ForgeManager Instance;

    [SerializeField] private CraftingRecipeConfig swordRecipe;

    public CraftingRecipeConfig SwordRecipe => swordRecipe;

    void Awake()
    {
        Instance = this;
    }

    public bool CanCraft(CraftingRecipeConfig recipe)
    {
        foreach (CraftingIngredient ingredient in recipe.ingredients)
        {
            if (MaterialInventory.Instance.GetCount(ingredient.materialName) < ingredient.amount)
                return false;
        }
        return true;
    }

    public bool Craft(CraftingRecipeConfig recipe)
    {
        if (!CanCraft(recipe)) return false;

        foreach (CraftingIngredient ingredient in recipe.ingredients)
            MaterialInventory.Instance.Deduct(ingredient.materialName, ingredient.amount);

        GearInventory.Instance.Add(recipe.itemName, 1);
        return true;
    }
}
#endregion