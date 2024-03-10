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
        degreesTurned = 0;
        mixing = false;
        started = false;
        prevDir = Vector2.zero;
        StartCoroutine(WaitAndStart());
    }

    private IEnumerator WaitAndStart()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        started = true;
    }
    
    private void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0) && IsInside())
        {
            SoundManager.Instance.PotStirInstance.start();
            mixing = true;
        }
        else if (Input.GetMouseButtonUp(0) && mixing)
        {
            if (degreesTurned < degreesTarget)
            {
                Debug.Log("durdu");
                SoundManager.Instance.PotStirInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                LoseMiniGame();
            }
        }
    }

    private float DistanceToCenter()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(area, Input.mousePosition, null, out var rectPos);
        var dist = rectPos.magnitude;
        return dist;
    }

    private bool IsInside()
    {
        var dist = DistanceToCenter();
        return dist > minDistance && dist < maxDistance;
    }

    private void CheckArea()
    {
        if (mixing)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(area, Input.mousePosition, null, out var rectPos);
            if (!IsInside())
            {
                Debug.Log("durdu");
                SoundManager.Instance.PotStirInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                LoseMiniGame();
            }
            if (degreesTurned >= degreesTarget)
            {
                SoundManager.Instance.PotStirInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                WinMiniGame();
            }
            if (prevDir != Vector2.zero)
            {
                var angle = Vector2.SignedAngle(prevDir, rectPos.normalized);
                degreesTurned -= angle;
            }
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