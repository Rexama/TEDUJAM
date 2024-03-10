using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MixingStation : Station
{
    [SerializeField]
    public List<Recipe> recipes;

    [SerializeField]
    public MiniGamePanel MixingMiniGamePanel;

    [SerializeField]
    public GameObject defPotion;

    public override List<Recipe> Recipes => recipes;

    public override StationType StationType => StationType.Mixing;

    public override MiniGamePanel MiniGamePanel => MixingMiniGamePanel;

    public virtual void ProduceRecipe()
    {
        var flag = true;
        foreach (var recipe in Recipes)
        {
            activeRecipe = recipe;

            var ingredients = new List<IngredientType>(activeRecipe.Ingredients);
            var ingredientsInside = draggableObjects.Where(obj => !(obj is Tool)).ToList();

            foreach (IngredientType ingredientType in ingredients)
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
                    flag =  false;
                }
            }

            if (flag && ingredients.Count != ingredientsInside.Count)
            {
                flag = false;
            }
        }

        //if (activeRecipe != null && flag)
        //{
        //    Instantiate(activeRecipe.EndProduct, transform.position, Quaternion.identity);
        //}
        //else
        //{
        //    Instantiate(defPotion, transform.position, Quaternion.identity);
        //}

        GameManager.Instance.EndPotionType = activeRecipe.EndProduct.GetComponent<Potion>().Type;
        Debug.Log(activeRecipe.EndProduct.GetComponent<Potion>().Type);

        draggableObjects.Clear();
        activeRecipe = null;
    }

    public virtual void CheckForPossibleRecipieStart()
    {
        if (draggableObjects.Any(x => x is Tool && (x as Tool).ToolType == ToolType.Spoon) && draggableObjects.Count>=2)
        {
            MiniGamePanel.OpenMiniGame();
        }

        foreach (Draggable draggableObject in draggableObjects)
        {
            if (draggableObject is not Tool)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gameplay/AddIngredientIntoPot");
                draggableObject.DeactivateVisual();
            }
        }
    }

    public virtual void ResetMixingStation()
    {
        draggableObjects.Clear();
    }
}
