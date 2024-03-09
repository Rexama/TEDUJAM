using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniGameManager : Singleton<MiniGameManager>
{
    [SerializeField]
    private MiniGamePanel CuttingMiniGame;

    [SerializeField]
    private MiniGamePanel CrushingMiniGame;

    [SerializeField]
    private MiniGamePanel MixingMiniGame;

    public void StartCuttingMiniGame()
    {
        CuttingMiniGame.OpenMiniGame();
    }

    public void StartCrushingMiniGame()
    {
        CrushingMiniGame.OpenMiniGame();
    }

    public void StartMixingMiniGame()
    {
        MixingMiniGame.OpenMiniGame();
    }

}