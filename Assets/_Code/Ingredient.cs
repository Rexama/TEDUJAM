using UnityEngine;

public enum IngredientType
{
    SolidRed,
    SolidBlue,
    SolidYellow,
    LiquidRed,
    LiquidBlue,
    LiquidYellow,

    SolidRedCuted,
    SolidYellowCutted,
    SolidBlueCutted,

    SolidRedCrushed,
    SolidYellowCrushed,
    SolidBlueCrushed,
}

public class Ingredient : Draggable
{
    [SerializeField]
    public IngredientType IngredientType;
}

