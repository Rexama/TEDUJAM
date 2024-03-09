using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingStation : Station
{
    [SerializeField]
    public List<Recipe> recipes;

    public override List<Recipe> Recipes => recipes;

    public override StationType StationType => StationType.Crushing;

    public override void StartRecipieMinigame()
    {
        throw new System.NotImplementedException();
    }
}
