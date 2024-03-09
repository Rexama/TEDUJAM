using DG.Tweening;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    [SerializeField]
    private Slider progressBar;

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
        }
    }

    private void CheckArea()
    {
        if (mixing)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(area, Input.mousePosition, null, out var rectPos);
            var dist = rectPos.magnitude;
            if (dist < minDistance || dist > maxDistance)
            {
                LoseMiniGame();
            }
            if (degreesTurned >= degreesTarget)
            {
                WinMiniGame();
            }
            var angle = Vector2.SignedAngle(prevDir, rectPos.normalized);
            degreesTurned -= angle;
            progressBar.SetValueWithoutNotify(Mathf.Clamp(degreesTurned, 0, float.MaxValue) / degreesTarget);
            prevDir = rectPos.normalized;
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