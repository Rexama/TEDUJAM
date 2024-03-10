using System.Collections.Generic;
using UnityEngine;

public class CatArea : MonoBehaviour
{
    public GameObject CatNest;
    public GameObject DropArea;
    public CatBody CatBody;

    private Draggable draggedObject;

    public void PutObjectCatArea(Draggable obj)
    {
        obj.transform.position = DropArea.transform.position;
        draggedObject = obj;
        obj.gameObject.SetActive(false);
    }

    public void CatInNest(bool inNest)
    {
        CatBody.gameObject.SetActive(inNest);
        if(draggedObject != null)
        {
            draggedObject.gameObject.SetActive(true);
            draggedObject = null;
        }
    }
    
}
