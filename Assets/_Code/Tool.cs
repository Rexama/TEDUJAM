using UnityEngine;

// Enum for tool types
public enum ToolType
{
    Knife,
    Spoon,
    Crusher
}

public class Tool : Draggable
{
    [SerializeField] public ToolType ToolType;

}