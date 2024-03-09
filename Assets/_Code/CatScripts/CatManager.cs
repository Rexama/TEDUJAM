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
    [SerializeField]
    public GameManager GameManager;

    private int _mischiefPeriod = 10;
    private int _restPeriod = 10;

    public CatStates CurrentState;

    private void Start()
    {
        StartCoroutine(StartMischief());
    }

    private IEnumerator StartMischief()
    {
        while (true)
        {
            yield return new WaitForSeconds(_restPeriod);

            CurrentState = CatStates.InNest;

            Debug.Log("In Nest");

            yield return new WaitForSeconds(_mischiefPeriod);

            CurrentState = CatStates.OutNest;
            Debug.Log("Out Nest");

            yield return new WaitForSeconds(_mischiefPeriod);

            CurrentState = CatStates.Disturbing;
            _startRandomMischief();

            Debug.Log("Disturbing");
        }
    }

    private void _startRandomMischief()
    {
        Array enumValues = Enum.GetValues(typeof(MischiefTypes));

        int randomIndex = UnityEngine.Random.Range(0, enumValues.Length);

        var selectedType = (MischiefTypes)enumValues.GetValue(randomIndex);

    }
    private void _doMischief()
    {

    }
}
