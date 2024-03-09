using UnityEngine;
using System.Collections.Generic;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Station currentStation;
    private Station oldStation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Station newStation = other.GetComponent<Station>();
        if (newStation != null && newStation != currentStation)
        {
            currentStation = newStation;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Station stati = other.GetComponent<Station>();
        if (currentStation != null && stati == currentStation)
        {
            currentStation = null;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;

            if (currentStation != null && currentStation.IsColliding(gameObject))
            {
                currentStation.AddToDraggables(this);
                oldStation = currentStation;
            }
            else if (oldStation != null && !oldStation.IsColliding(gameObject))
            {
                oldStation.RemoveFromDraggables(this);
            }
        }
    }
}