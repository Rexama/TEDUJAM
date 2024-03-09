using UnityEngine;

public enum IngredientType
{
    SolidRed,
    SolidBlue,
    SolidYellow,
    LiquidRed,
    LiquidBlue,
    LiquidYellow
}

public class Ingredient : Draggable
{
    [SerializeField]
    public IngredientType IngredientType;
}

