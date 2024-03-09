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

    private float _enteringTime;
    private float _exitingTime;

    private Vector3 _initialPosition;
    private Vector3 _targetPosition;

    private float _timeToReach;

    private float _shiftAmount = 2;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 targetPos = transform.position;
        if (_enteringTime > 0)
        {
            _enteringTime -= Time.deltaTime;
            float t = Mathf.Clamp01(_enteringTime / _timeToReach);
            targetPos = Vector3.Lerp(_targetPosition, _initialPosition, t);
        }
        else if (_exitingTime > 0)
        {
            _exitingTime -= Time.deltaTime;
            float t = Mathf.Clamp01(_exitingTime / _timeToReach);
            targetPos = Vector3.Lerp(_initialPosition,_targetPosition, t);
            if(_exitingTime <= 0)
            {
                _onHeadGiveUp.Invoke();
                Destroy(gameObject);
            }
        }
        transform.position = new Vector3(Camera.main.transform.position.x, targetPos.y, targetPos.z);
    }
    internal void Setup(Action onHeadGiveUp)
    {
        _onHeadGiveUp = onHeadGiveUp;
        _initialPosition = new Vector3(0, GameManager.Instance.ScreenTopEdgeY ,0);
        _targetPosition = new Vector3(0, GameManager.Instance.ScreenTopEdgeY - _shiftAmount, 0);

        _originalColor = _spriteRenderer.color;
        _clickNumber = 0;
        _enteringTime = 3;
        _timeToReach = 3;
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
            _enteringTime = 0;
            _targetPosition = transform.position;
            _exitingTime = 2;
            //_timeToReach = 2;
        }
    }
    private void _hitEffect()
    {
        _spriteRenderer.DOColor(Color.red, 0.1f);
        StartCoroutine(_revertColorAfterDelay());
    }
    private IEnumerator _revertColorAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.DOColor(_originalColor, 0.1f);
    }
}
