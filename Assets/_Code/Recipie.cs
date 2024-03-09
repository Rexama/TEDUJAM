using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public Station RequiredStation;
    public Tool RequiredTool;
    public List<Ingredient> Ingredients;
}