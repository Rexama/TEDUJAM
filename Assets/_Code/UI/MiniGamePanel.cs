using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public abstract class MiniGamePanel : UIBehaviour
{
    public bool isActive = false;

    public abstract void StartGame();

    public abstract Station Station { get; }

    public void OpenMiniGame()
    {
        if(!isActive)
        {
            gameObject.SetActive(true);
            StartGame();
            isActive = true;
        }
    }

    public void WinMiniGame()
    {
        gameObject.SetActive(false);
        if(Station is MixingStation) 
        {
            (Station as MixingStation).ProduceRecipe();
        }
        else
        {
            Station.ProduceRecipe();
        }
    }

    public void LoseMiniGame()
    {
        gameObject.SetActive(false);
    }
}