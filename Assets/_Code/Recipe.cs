using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public StationType RequiredStation;
    public ToolType RequiredTool;
    public List<IngredientType> Ingredients;

    public GameObject EndProduct;
}