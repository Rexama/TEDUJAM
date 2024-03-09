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

        if (recipe.RequiredTool != null && !draggableObjects.Any(x => x is Tool && (x as Tool).ToolType== recipe.RequiredTool ))
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

    public bool IsColliding(GameObject obj)
    {
        Collider2D collider = obj.GetComponent<Collider2D>();
        return collider != null && collider.IsTouching(GetComponent<Collider2D>());
    }

    public void AddToDraggables(Draggable obj)
    {
        draggableObjects.Add(obj);
        Debug.Log(obj.name + " added to station.");
        CheckForPossibleRecipieStart(Recipes);
    }

    public void RemoveFromDraggables(Draggable obj)
    {
        draggableObjects.Remove(obj);
        Debug.Log(obj.name + " added to station.");
    }
}
