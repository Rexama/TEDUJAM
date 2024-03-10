using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> CollisionList;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionList.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CollisionList.Remove(other.gameObject);
    }

    private void OnDisable()
    {
        CollisionList.Clear();
    }
}