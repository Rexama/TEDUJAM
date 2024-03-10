using Unity.VisualScripting;
using UnityEngine;

public class MouseInput : Singleton<MouseInput>
{
    public int currentOrder = 5;

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
                    if(station is MixingStation)
                    {
                        (station as MixingStation).CheckForPossibleRecipieStart();
                    }
                    else
                    {
                        station.CheckForPossibleRecipieStart();
                    }
                };
            }
        }
    }
}