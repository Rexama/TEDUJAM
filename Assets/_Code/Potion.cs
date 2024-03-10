using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType
{
    PotionType1,
    PotionType2,
    PotionType3,
    PotionType4,
    PotionBad
}

public class Potion : Draggable
{
   [SerializeField]
   public PotionType Type;
}
