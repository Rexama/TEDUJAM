using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    public List<Potion> PotionsInside = new List<Potion>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        Potion potion = other.GetComponent<Potion>();
        if (potion != null)
        {
            PotionsInside.Add(potion);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Potion potion = other.GetComponent<Potion>();
        if (potion != null)
        {
            PotionsInside.Remove(potion);
        }
    }
}

