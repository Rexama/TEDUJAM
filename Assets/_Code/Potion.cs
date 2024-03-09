using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType
{
    PotionType1,
    PotionType2,
    PotionType3,
    PotionType4,
    PotionType5,
}

public class Potion : Draggable
{
   [SerializeField]
   public PotionType Type;
}
