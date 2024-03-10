using System.Net.NetworkInformation;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPosition();
        isDragging = true;
        _playGrabSound();
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPosition() + offset;
            transform.position = newPosition;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public void DeactivateVisual()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = false;
        }
    }

    public void CloseSpriteRenderers(Transform parentTransform)
    {
        SpriteRenderer[] spriteRenderers = parentTransform.GetComponentsInChildren<SpriteRenderer>(true);
        var its = gameObject.GetComponent<SpriteRenderer>();
        if(its != null)
            its.enabled = false;

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.enabled = false;
        }
    }

    private void _playGrabSound()
    {
        var ing = gameObject.GetComponent<Ingredient>();
        if (ing != null)
        {
            if(ing.IngredientType == IngredientType.LiquidRed || ing.IngredientType == IngredientType.LiquidYellow || ing.IngredientType == IngredientType.LiquidBlue)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gameplay/GrabBottle");
            }
            else if(ing.IngredientType == IngredientType.SolidTeeth)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gameplay/GrabCanineTeeth");
            }
            else if(ing.IngredientType == IngredientType.SolidCyristal)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gameplay/GrabCrystal");
            }
            else if(ing.IngredientType == IngredientType.SolidMushroom)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gameplay/GrabMushroom");
            }
        }
    }
}