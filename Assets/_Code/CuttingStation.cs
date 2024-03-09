using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingStation : Station
{
    [SerializeField]
    public List<Recipe> recipes;

    public override List<Recipe> Recipes => recipes;

    public override StationType StationType => StationType.Cutting;

    public override void StartRecipieMinigame()
    {
        MiniGameManager.Instance.StartCuttingMiniGame();
    }
}
