using DG.Tweening;
using UnityEngine;


public class VaporEffect : Draggable
{
    public SpriteRenderer vapor1;
    public SpriteRenderer vapor2;


    public float fromAlpha = 255f;
    public float toAlpha = 100f;
    public float duration = 2f;
    public float duration2 = 2f;

    void Start()
    {
        LoopAlpha(vapor1, fromAlpha ,toAlpha,duration);
        LoopAlpha(vapor2, toAlpha, fromAlpha, duration2);
    }

    void LoopAlpha(SpriteRenderer spriteRenderer,float firstTarget, float secondTarget,float duration)
    {
        spriteRenderer.DOFade(firstTarget / 255f, duration).From(secondTarget / 255f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}