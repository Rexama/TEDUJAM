using Unity.VisualScripting;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);

            foreach (RaycastHit2D hit in hits)
            {
                Station station;
                if (hit.collider.gameObject.TryGetComponent(out station)) 
                {
                    station.CheckForPossibleRecipieStart();
                };
            }
        }
    }
}