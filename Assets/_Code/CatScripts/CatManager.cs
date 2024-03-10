using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public enum CatStates
{
    InNest,
    Going,
    Disturbing,
    Returning,
}
public enum MischiefTypes
{
    StealKnife,
    StealSpoon,
    StealCrusher,
    GetTopOfHead,
}
public class CatManager : MonoBehaviour
{
    public GameManager GameManager;

    //Prefabs
    public CatHand CatHandPrefabUp;
    public CatHand CatHandPrefabDown;
    public CatHead CatHeadPrefab;
    public CatTail CatTailPrefab;

    public CatArea CatArea;

    private int _inNestPeriod = 10;
    private int _outNestPeriod = 10;


    private CatStates _currentState;
    public CatStates CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gameplay/CatMeow");
            _stateManage();            
        }
    }

    private void Start()
    {
        CurrentState = CatStates.InNest;
    }

    private IEnumerator _startGoing()
    {
        yield return new WaitForSeconds(_inNestPeriod);
        CurrentState = CatStates.Going;
    }
    private void _stateManage()
    {
        if(CurrentState == CatStates.InNest)
        {
            CatArea.CatInNest(true);
            StartCoroutine(_startGoing());
        }
        else if (CurrentState == CatStates.Going)
        {
            CatArea.CatInNest(false);
            _selectedTargetX = _selectMischief();
            _moveTailFromNest();

        }
        else if (CurrentState == CatStates.Disturbing)
        {
            _startMischief();
        }
        else if (CurrentState == CatStates.Returning)
        {
            _moveTailToNest();
        }
        
    }
    private void _startMischief()
    {
        switch (_selectedMischief)
        {
            case MischiefTypes.StealKnife:
                _stealObject(GameManager.Instance.Knife);
                break;
            case MischiefTypes.StealSpoon:
                _stealObject(GameManager.Instance.Spoon);
                break;
            case MischiefTypes.StealCrusher:
                _stealObject(GameManager.Instance.Crusher);
                break;
            case MischiefTypes.GetTopOfHead:
                _getTopOfHead();
                break;
            default:
                break;
        }
    }

    private MischiefTypes _selectedMischief;
    private float _selectedTargetX;
    private float _selectMischief()
    {
        Array enumValues = Enum.GetValues(typeof(MischiefTypes));

        int randomIndex = UnityEngine.Random.Range(0, enumValues.Length);

        var selectedType = (MischiefTypes)enumValues.GetValue(randomIndex);

        _selectedMischief = selectedType;

        switch (selectedType)
        {
            case MischiefTypes.StealKnife:
                return GameManager.Knife.transform.position.x;
            case MischiefTypes.StealSpoon:
                return GameManager.Spoon.transform.position.x;
            case MischiefTypes.StealCrusher:
                return GameManager.Crusher.transform.position.x;
            case MischiefTypes.GetTopOfHead:
                return 0;
            default:
                return 0;
        }

    }


    private void _moveTailFromNest()
    {
        var catTail = Instantiate(CatTailPrefab);
        catTail.transform.position = new Vector2(CatArea.transform.position.x -1, GameManager.ScreenTopEdgeY-2);
        catTail.transform.DOMoveX(_selectedTargetX, _outNestPeriod).SetEase(Ease.OutSine).OnComplete(() =>
        {
            CurrentState = CatStates.Disturbing;
            Destroy(catTail.gameObject);
        });
    }
    private void _moveTailToNest()
    {
        var catTail = Instantiate(CatTailPrefab);
        catTail.transform.DORotate(new Vector3(0, 180, 0), 0.1f);
        catTail.transform.position = new Vector2(_selectedTargetX, GameManager.ScreenTopEdgeY-2);
        catTail.transform.DOMoveX(CatArea.transform.position.x -2, _outNestPeriod).SetEase(Ease.OutSine).OnComplete(() =>
        {
            CurrentState = CatStates.InNest;
            Destroy(catTail.gameObject);
        });
    }
    private void _stealObject(Draggable obj)
    {
        var initialPosition = _decideInitialPosition(obj.transform.position);
        CatHand handPrefab;
        if(initialPosition.y > 4.2)
        {
            handPrefab = CatHandPrefabDown;
        }else
        {
            handPrefab = CatHandPrefabUp;
        }
        var catHand = Instantiate(handPrefab);
        
        catHand.Setup(obj,initialPosition, 3, _onHandArrive,_onPullHandCompleted);
    }
    private Vector3 _decideInitialPosition(Vector3 targetPos)
    {
        var bottom = GameManager.Instance.ScreenBottomEdgeY;
        var top = GameManager.Instance.ScreenTopEdgeY;
        int result;
        if (targetPos.y < 0)
        {
            result = bottom;
        }
        else
        {
            result = top;
        }
        return new Vector3(targetPos.x, result, targetPos.z);
    }
    private void _getTopOfHead()
    {
        var catHand = Instantiate(CatHeadPrefab);
        catHand.Setup(_onHeadGiveUp);
    }
    private void _onHeadGiveUp()
    {
        CurrentState = CatStates.Returning;
    }
    private void _onHandArrive()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Gameplay/CatPaw");
    }
    private void _onPullHandCompleted()
    {
        CurrentState = CatStates.Returning;
    }
    private void _doMischief()
    {

    }
}
