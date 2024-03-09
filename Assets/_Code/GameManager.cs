using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Station CuttingStation;
    public Station MixingStation;
    public Station CrushingStation;
    public CatManager CatManager;


    public Tool Knife;
    public Tool Spoon;
    public Tool Crusher;

    public int ScreenTopEdgeY = 5 ;




}
