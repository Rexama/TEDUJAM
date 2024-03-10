using UnityEngine;

public enum IngredientType
{
    SolidTeeth,
    SolidMushroom,
    SolidCyristal,
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

