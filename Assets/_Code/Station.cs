using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Station : MonoBehaviour
{
    private List<Draggable> draggableObjects = new List<Draggable>();
    private List<Tool> tools = new List<Tool>();

    //private abstract void StartRecipieMinigame();

    private bool CanRecipeStart(Recipe recipe)
    {
        if (recipe.RequiredStation != null && recipe.RequiredStation != this)
        {
            Debug.Log("Recipe requires a different station.");
            return false;
        }

        if (recipe.RequiredTool != null && !tools.Contains(recipe.RequiredTool))
        {
            Debug.Log("Recipe requires a different tool.");
            return false;
        }

        foreach (Ingredient ingredient in recipe.Ingredients)
        {
            bool ingredientFound = false;
            foreach (Draggable draggableObject in draggableObjects)
            {
                if (draggableObject is Ingredient ingredientObject && ingredientObject.IngredientType == ingredient.IngredientType)
                {
                    ingredientFound = true;
                    break;
                }
            }

            if (!ingredientFound)
            {
                Debug.Log("Missing ingredient: " + ingredient.IngredientType);
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
            // Add the draggable object to the list
            draggableObjects.Add(draggable);
            // Perform any desired action here
            Debug.Log("Draggable object placed on station.");
        }

        Tool tool = other.GetComponent<Tool>();
        if (tool != null && !tools.Contains(tool))
        {
            // Add the tool object to the list
            tools.Add(tool);
            // Perform any desired action here
            Debug.Log("Tool placed on station.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Draggable draggable = other.GetComponent<Draggable>();
        if (draggable != null && draggableObjects.Contains(draggable))
        {
            // Remove the draggable object from the list
            draggableObjects.Remove(draggable);
            // Perform any desired action here
            Debug.Log("Draggable object removed from station.");
        }

        Tool tool = other.GetComponent<Tool>();
        if (tool != null && tools.Contains(tool))
        {
            // Remove the tool object from the list
            tools.Remove(tool);
            // Perform any desired action here
            Debug.Log("Tool removed from station.");
        }
    }
}
