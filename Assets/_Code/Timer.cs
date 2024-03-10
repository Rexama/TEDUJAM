using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    
    private float _timeRemaining = 181;

    [SerializeField]
    TextMeshProUGUI _timeText;

    void Update()
    {
        if (_timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
            DisplayTime();
        }else
        {
            GameManager.Instance.TimeUpEnding();
        }
    }
    void DisplayTime()
    {
        float minutes = Mathf.FloorToInt(_timeRemaining / 60);
        float seconds = Mathf.FloorToInt(_timeRemaining % 60);

        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}