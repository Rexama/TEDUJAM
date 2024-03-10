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
    public CatHand CatHandPrefab;
    public CatHead CatHeadPrefab;
    public CatTail CatTailPrefab;

    public CatArea CatArea;

    private int _inNestPeriod = 5;
    private int _outNestPeriod = 5;


    private CatStates _currentState;
    public CatStates CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
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
            Debug.Log("In Nest");
        }
        else if (CurrentState == CatStates.Going)
        {
            CatArea.CatInNest(false);
            _selectedTargetX = _selectMischief();
            _moveTailFromNest();
            Debug.Log("Going");

        }
        else if (CurrentState == CatStates.Disturbing)
        {
            _startMischief();
            Debug.Log("disturbing");
        }
        else if (CurrentState == CatStates.Returning)
        {
            _moveTailToNest();
            Debug.Log("returning");
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
        catTail.transform.position = new Vector2(CatArea.transform.position.x, GameManager.ScreenTopEdgeY);
        catTail.transform.DOMoveX(_selectedTargetX, _outNestPeriod).SetEase(Ease.OutSine).OnComplete(() =>
        {
            CurrentState = CatStates.Disturbing;
            Destroy(catTail.gameObject);
        });
    }
    private void _moveTailToNest()
    {
        var catTail = Instantiate(CatTailPrefab);
        catTail.transform.position = new Vector2(_selectedTargetX, GameManager.ScreenTopEdgeY);
        catTail.transform.DOMoveX(CatArea.transform.position.x, _outNestPeriod).SetEase(Ease.OutSine).OnComplete(() =>
        {
            CurrentState = CatStates.InNest;
            Destroy(catTail.gameObject);
        });
    }
    private void _stealObject(Draggable obj)
    {
        var catHand = Instantiate(CatHandPrefab);
        catHand.Setup(obj, 3, _onHandArrive,_onPullHandCompleted);
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

    }
    private void _onPullHandCompleted()
    {
        CurrentState = CatStates.Returning;
    }
    private void _doMischief()
    {

    }
}
