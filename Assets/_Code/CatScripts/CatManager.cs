using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public enum CatStates
{
    InNest,
    OutNest,
    Disturbing
}
public enum MischiefTypes
{
    StealKnife
}
public class CatManager : MonoBehaviour
{
    public GameManager GameManager;
    public CatHand CatHandPrefab;
    public CatHead CatHeadPrefab;

    public CatArea CatArea;


    private int _disturbingPeriod = 15;
    private int _inNestPeriod = 1;
    private int _outNestPeriod = 10;

    public CatStates CurrentState;

    private void Start()
    {
        StartCoroutine(StartMischief());
    }

    private IEnumerator StartMischief()
    {
        while (true)
        {
            yield return new WaitForSeconds(_inNestPeriod);

            CurrentState = CatStates.InNest;
            _startRandomMischief();
            Debug.Log("In Nest");

            yield return new WaitForSeconds(_outNestPeriod);

            CurrentState = CatStates.OutNest;
            Debug.Log("Out Nest");

            yield return new WaitForSeconds(_disturbingPeriod);

            CurrentState = CatStates.Disturbing;
            _startRandomMischief();

            Debug.Log("Disturbing");

            yield return new WaitForSeconds(_outNestPeriod);

            CurrentState = CatStates.OutNest;
            Debug.Log("Out Nest");
        }
    }

    private void _startRandomMischief()
    {
        Array enumValues = Enum.GetValues(typeof(MischiefTypes));

        int randomIndex = UnityEngine.Random.Range(0, enumValues.Length);

        var selectedType = (MischiefTypes)enumValues.GetValue(randomIndex);

        _stealKnife();
        _getTopOfWitch();

    }

    private void _stealKnife()
    {
        var catHand = Instantiate(CatHandPrefab);
        catHand.Setup(GameManager.Instance.Knife, 3, _onHandArrive,_onPullHandCompleted);
    }
    private void _getTopOfWitch()
    {
        var catHand = Instantiate(CatHeadPrefab);
        catHand.Setup(_onHeadGiveUp);
    }
    private void _onHeadGiveUp()
    {

    }
    private void _onHandArrive()
    {

    }
    private void _onPullHandCompleted()
    {

    }
    private void _doMischief()
    {

    }
}
