using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CatHand : MonoBehaviour
{
    private Draggable _targetObject;

    private Vector3 _initialPosition;
    private Vector3 _targetPosition;
    private Vector3 _retreatPosition;


    private bool _isRetreating;
    private bool _firstPhase;

    private float _pause = 0;
    private int _touchTime = 0;

    private float _timeToReach;
    private float _elapsedTime;

    private System.Action _onHandArrive;
    private System.Action _onPullBack;

    private void _handleStates()
    {
        if (_pause > 0) //Pause State
        {
            _pause -= Time.deltaTime;
            return;
        }

        _elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(_elapsedTime / _timeToReach);

        if (_isRetreating)
        {
            var targetPos = Vector3.Lerp(_retreatPosition, _initialPosition, t);
            transform.position = targetPos;
            if (t >= 1.0f)
            {
                _onPullBack.Invoke();
                Destroy(gameObject);
            }
            
        }
        else
        {            
            if (_firstPhase)
            {
                if (_targetObject.transform.position != _targetPosition)
                {
                    _startRetreat();
                    return;
                }
                transform.position = Vector3.Lerp(_initialPosition, _targetPosition, t);

                if (t >= 1f)
                {
                    _onHandArrive.Invoke();
                    _firstPhase = false;
                    _elapsedTime = 0f;

                }
            }
            else
            {
                var targetPos = Vector3.Lerp(_targetPosition, _initialPosition, t);
                transform.position = targetPos;
                _targetObject.transform.position = targetPos;
                if (t >= 1.0f)
                {
                    _onPullBack.Invoke();
                    GameManager.Instance.CatManager.CatArea.PutObjectCatArea(_targetObject);
                    Destroy(gameObject);
                }

            }
        }
        
    }
    private void Update()
    {
        _handleStates();
    }

    public void Setup(Draggable targetObject,Vector3 initialPos,int time,System.Action onHandArrive,System.Action onPullBack = null)
    {
        _targetObject = targetObject;
        _targetPosition = targetObject.transform.position;
        _initialPosition = initialPos;
        _elapsedTime = 0f;
        _timeToReach = time;
        _onHandArrive = onHandArrive;
        _onPullBack = onPullBack;
        _firstPhase=true;

    }
    private void _startRetreat()
    {
        _retreatPosition = transform.position;
        _elapsedTime = 0;
        _isRetreating = true;
    }
    public void OnMouseDown()
    {
        if(_pause > 0 || _isRetreating) return;

        if (_touchTime > 0 || !_firstPhase)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gameplay/CatPaw");
            _startRetreat();
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gameplay/CatPaw");
            _pause = 2;
            _touchTime++;
        }        
    }
}
