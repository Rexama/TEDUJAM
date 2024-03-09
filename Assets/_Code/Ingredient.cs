using UnityEngine;

public enum IngredientType
{
    SolidRed,
    SolidBlue,
    SolidYellow,
    LiquidRed,
    LiquidBlue,
    LiquidYellow,

    SolidRedBlueCutted,
    SolidRedYellowCutted,
    SolidYellowBlueCutted,
    SolidRedYellowBlueCutted,

    SolidRedBlueCrushed,
    SolidRedYellowCrushed,
    SolidYellowBlueCrushed,
    SolidRedYellowBlueCrushed,
}

public class Ingredient : Draggable
{
    [SerializeField]
    public IngredientType IngredientType;
}

