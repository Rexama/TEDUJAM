using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CatHead : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private Color _originalColor;
    private Action _onHeadGiveUp;

    private int _clickNumber;
    private Camera _mainCamera;

    private Vector3 _initialPosition;

    private float _timeToReach = 5;
    private float _timeToRetreat = 3;

    private float _shiftAmount = 2;

    bool _startOnWitchAnim;

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        transform.DOMoveX(_mainCamera.transform.position.x, 0.1f);
    }
    internal void Setup(Action onHeadGiveUp)
    {
        _onHeadGiveUp = onHeadGiveUp;
        transform.position = new Vector3(0, GameManager.Instance.ScreenTopEdgeY +1 ,0);
        _initialPosition = transform.position;

        transform.DOMoveY(transform.position.y - _shiftAmount, _timeToReach).SetEase(Ease.InElastic).OnComplete(() =>
        {
            _startOnWitchAnim = true;
        }); ;
        _originalColor = _spriteRenderer.color;
        _clickNumber = 0;
    }
    public void OnMouseDown()
    {
        _clickNumber++;
        if(_clickNumber < 5)
        {
            _hitEffect();
        }
        else if( _clickNumber == 5)
        {
            transform.DOMoveY(_initialPosition.y+2, _timeToRetreat)
                .SetEase(Ease.OutQuint)
                    .OnComplete(() =>
                        {
                            _onHeadGiveUp.Invoke();
                            Destroy(gameObject);
                        });
        }
    }
    private void _hitEffect()
    {
        _spriteRenderer.DOColor(Color.red, 0.1f);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gameplay/CatPaw");
        StartCoroutine(_revertColorAfterDelay());
    }
    private IEnumerator _revertColorAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.DOColor(_originalColor, 0.1f);
    }
}
