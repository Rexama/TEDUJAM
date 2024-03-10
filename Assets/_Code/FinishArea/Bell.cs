using System;
using UnityEngine;

public class Bell : MonoBehaviour
{
    public Action OnBellPressed;

    private void Start()
    {
        var box = gameObject.GetComponent<BoxCollider2D>();
        box.isTrigger = true;
    }
    private void OnMouseDown()
    {
        Debug.Log("pressed");
        OnBellPressed.Invoke();
    }
}
