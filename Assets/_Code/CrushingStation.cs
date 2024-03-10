using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingStation : Station
{
    [SerializeField]
    public List<Recipe> recipes;

    [SerializeField]
    public MiniGamePanel miniGamePanel;

    public override List<Recipe> Recipes => recipes;

    public override StationType StationType => StationType.Crushing;

    public override MiniGamePanel MiniGamePanel => miniGamePanel;
}
