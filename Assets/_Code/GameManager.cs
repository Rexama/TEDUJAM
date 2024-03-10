using System.Collections.Generic;
using UnityEngine;

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


    public void Start()
    {

    }
    public List<Ingredient> GetIngredients()
    {
        Ingredient[] ingredientObjects = GameObject.FindObjectsOfType<Ingredient>();
        return new List<Ingredient>(ingredientObjects);
    }

}
