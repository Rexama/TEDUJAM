using UnityEngine;

public class CatArea : MonoBehaviour
{
    public GameObject CatNest;
    public GameObject DropArea;
    public CatBody CatBody;

    
    public void CatInNest(bool inNest)
    {
        CatBody.gameObject.SetActive(inNest);
    }
    
}
