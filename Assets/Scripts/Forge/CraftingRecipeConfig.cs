using UnityEngine;

[System.Serializable]
public class CraftingIngredient
{
    public string materialName;
    public int amount;
}

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "TheOldWorld/CraftingRecipe")]
public class CraftingRecipeConfig : ScriptableObject
{
    public string itemName;
    public CraftingIngredient[] ingredients;
}