using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<Ingredient> IngredientList;

    public Station CuttingStation;
    public Station MixingStation;
    public Station CrushingStation;
    public CatManager CatManager;


    public Tool Knife;
    public Tool Spoon;
    public Tool Crusher;

    public int ScreenTopEdgeY = 5 ;
    public int ScreenBottomEdgeY = -5;

    public PotionType EndPotionType;


    public List<Ingredient> GetIngredients()
    {
        Ingredient[] ingredientObjects = GameObject.FindObjectsOfType<Ingredient>();
        return new List<Ingredient>(ingredientObjects);
    }


    //Endings
    public void PotionCraftedEnding()
    {
        if (false)
        {
            if (false)
            {
                SceneManager.LoadScene(1);// Bad Ending
            }
            else
            {
                SceneManager.LoadScene(2);// Good Ending
            }
        }
    }    
    public void TimeUpEnding()
    {
        SceneManager.LoadScene(1); // Bad Ending
    }

}
