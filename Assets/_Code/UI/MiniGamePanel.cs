using System;
using UnityEngine.EventSystems;

public abstract class MiniGamePanel : UIBehaviour
{
    public abstract void StartGame();

    public abstract Station Station { get; }

    public void OpenMiniGame()
    {
        gameObject.SetActive(true);
        StartGame();
    }

    public void WinMiniGame()
    {
        gameObject.SetActive(false);
        Station.ProduceRecipe();
    }

    public void LoseMiniGame()
    {
        gameObject.SetActive(false);
    }
}