using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingStation : Station
{
    [SerializeField]
    public List<Recipe> recipes;

    [SerializeField]
    public MiniGamePanel MixingMiniGamePanel;


    public override List<Recipe> Recipes => recipes;

    public override StationType StationType => StationType.Mixing;

    public override MiniGamePanel MiniGamePanel => MixingMiniGamePanel;
}
