using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinishArea : MonoBehaviour
{
    public Bell Bell;
    public DropArea DropArea;

    void Start()
    {
        Bell.OnBellPressed = _ringBell;
    }

    private void _ringBell()
    {
        Debug.Log(DropArea.PotionsInside.Count);
        if(DropArea.PotionsInside.Count == 1)
        {
            var finalPotion = DropArea.PotionsInside.First();
        }
    }
}
