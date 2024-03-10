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
    protected List<Draggable> draggableObjects = new List<Draggable>();
    public abstract List<Recipe> Recipes { get; }
    public abstract StationType StationType { get; }
    public abstract MiniGamePanel MiniGamePanel { get; }


    protected Recipe activeRecipe;
    public virtual void ProduceRecipe()
    {
        var recipeIngredients = new List<IngredientType>(activeRecipe.Ingredients);

        var list = new List<Ingredient>();
        foreach (Draggable draggableObject in draggableObjects)
        {
            if (draggableObject is Ingredient ingredientObject && recipeIngredients.Contains(ingredientObject.IngredientType))
            {
                list.Add(ingredientObject);
                recipeIngredients.Remove(ingredientObject.IngredientType);
            }
        }

        foreach(var ing in list)
        {
            draggableObjects.Remove(ing);
            Destroy(ing.gameObject);
        }

        var craftedItem = Instantiate(activeRecipe.EndProduct,transform.position, Quaternion.identity);
        craftedItem.transform.position = new Vector3(craftedItem.transform.position.x, craftedItem.transform.position.y, -3f);
    }

    public virtual void CheckForPossibleRecipieStart()
    {
        foreach (var recipe in Recipes)
        {
            if (CanRecipeStart(recipe))
            {
                MiniGamePanel.OpenMiniGame();
                activeRecipe = recipe;
                break;
            }
        }
    }

    public virtual bool CanRecipeStart(Recipe recipe)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Draggable draggable = other.GetComponent<Draggable>();
        if (draggable != null && !draggableObjects.Contains(draggable))
        {
            draggableObjects.Add(draggable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Draggable draggable = other.GetComponent<Draggable>();
        if (draggable != null && draggableObjects.Contains(draggable))
        {
            draggableObjects.Remove(draggable);
        }
    }
}
