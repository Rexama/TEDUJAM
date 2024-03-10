using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CrushDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    private Vector3 offset;

    public UnityEvent onDragStartEvent;
    public UnityEvent onDragEndEvent;

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            // Update the object's position based on the mouse position
            Vector3 newPosition = Input.mousePosition + offset;
            transform.position = newPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Start dragging when the pointer is pressed down on the UI element
        StartDragging(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Stop dragging when the pointer is released
        StopDragging();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Update the object's position during dragging
        if (isDragging)
        {
            Vector3 newPosition = Input.mousePosition + offset;
            transform.position = newPosition;
        }
    }

    void StartDragging(PointerEventData eventData)
    {
        isDragging = true;

        // Calculate the offset between the object's position and the mouse position
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out offset
        );
        offset -= transform.position;

        // Fire the onDragStartEvent when dragging starts
        if (onDragStartEvent != null)
        {
            onDragStartEvent.Invoke();
        }
    }

    void StopDragging()
    {
        isDragging = false;

        // Fire the onDragEndEvent when dragging stops
        if (onDragEndEvent != null)
        {
            onDragEndEvent.Invoke();
        }
    }
}
