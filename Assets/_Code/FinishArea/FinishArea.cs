using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinishArea : MonoBehaviour
{
    public Bell Bell;
    void Start()
    {
        Bell.OnBellPressed = _ringBell;
    }

    private void _ringBell()
    {
        GameManager.Instance.PotionCraftedEnding();
    }
}
