using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;

public enum StationType
{
    Cutting,
    Crushing,
    Mixing
}

public abstract class Station : MonoBehaviour
{
    private List<Draggable> draggableObjects = new List<Draggable>();
    private List<Tool> tools = new List<Tool>();

    public abstract List<Recipe> Recipes { get; }
    public abstract StationType StationType { get; }

    public abstract void StartRecipieMinigame();

    public void CheckForPossibleRecipieStart(List<Recipe> recipies)
    {
        foreach (var recipe in recipies)
        {
            if (CanRecipeStart(recipe))
            {
                Debug.Log("recipie started");
                StartRecipieMinigame();
            }
        }
    }

    private bool CanRecipeStart(Recipe recipe)
    {
        if (recipe.RequiredStation != null && recipe.RequiredStation != StationType)
        {
            throw new System.NotImplementedException("recipie is not for this station");
        }

        if (recipe.RequiredTool != null && !tools.Any(x => x.ToolType == recipe.RequiredTool))
        {
            return false;
        }


        foreach (IngredientType ingredientType in recipe.Ingredients)
        {
            bool ingredientFound = false;
            foreach (Draggable draggableObject in draggableObjects)
            {
                if (draggableObject is Ingredient ingredientObject && ingredientObject.IngredientType == ingredientType)
                {
                    ingredientFound = true;
                    break;
                }
            }

            if (!ingredientFound)
            {
                return false;
            }
        }

        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Draggable draggable = other.GetComponent<Draggable>();
        if (draggable != null && !draggableObjects.Contains(draggable))
        {
            draggableObjects.Add(draggable);
            Debug.Log("Draggable object placed on station.");
        }

        Tool tool = other.GetComponent<Tool>();
        if (tool != null && !tools.Contains(tool))
        {
            tools.Add(tool);
            Debug.Log("Tool placed on station.");
        }
        CheckForPossibleRecipieStart(Recipes);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Draggable draggable = other.GetComponent<Draggable>();
        if (draggable != null && draggableObjects.Contains(draggable))
        {
            draggableObjects.Remove(draggable);
            Debug.Log("Draggable object removed from station.");
        }

        Tool tool = other.GetComponent<Tool>();
        if (tool != null && tools.Contains(tool))
        {
            tools.Remove(tool);
            Debug.Log("Tool removed from station.");
        }
    }
}
