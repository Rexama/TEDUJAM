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

    public CatArea CatArea;


    private int _disturbingPeriod = 10;
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

        InvokeStealKnife();

    }
    private void InvokeStealKnife()
    {
        var catHand = Instantiate(CatHandPrefab);
        catHand.Setup(GameManager.Instance.Knife, 3, _onHandArrive,_onSteal);
    }
    private void _onHandArrive(Draggable draggableObject)
    {

    }
    private void _onSteal(Draggable draggableObject)
    {
        draggableObject.transform.position = CatArea.DropArea.transform.position;
    }
    private void _doMischief()
    {

    }
}
