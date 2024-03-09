using DG.Tweening;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class MixingMiniGame : MiniGamePanel
{
    [SerializeField]
    public Station station;

    public override Station Station => station;

    private bool mixing;

    private bool started;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private float maxDistance;

    public float degreesTurned;

    [SerializeField]
    private float degreesTarget;

    private Vector2 prevDir;

    [SerializeField]
    private RectTransform area;

    public override void StartGame()
    {
        StartCoroutine(WaitAndStart());
    }

    private IEnumerator WaitAndStart()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        started = true;
    }
    
    private void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mixing = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (degreesTurned < degreesTarget)
            {
                LoseMiniGame();
            }
            else
            {
                WinMiniGame();
            }
        }
    }

    private void CheckArea()
    {
        if (mixing)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(area, Input.mousePosition, null, out var rectPos);
            Debug.Log(Vector2.Distance(area.position, rectPos));
        }
    }

    private void Update()
    {
        if (started)
        {
            CheckMouse();
            CheckArea();
        }
    }

}