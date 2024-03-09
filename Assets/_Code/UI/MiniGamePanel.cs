using System;
using UnityEngine.EventSystems;

public abstract class MiniGamePanel : UIBehaviour
{
    public abstract void StartGame();

    public void OpenMiniGame()
    {
        gameObject.SetActive(true);
    }

    public void CloseMiniGame()
    {
        gameObject.SetActive(false);
    }
}