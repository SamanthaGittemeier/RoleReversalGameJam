using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _timerText;

    [SerializeField]
    private int _timerMins;
    [SerializeField]
    private int _timerSecs;

    [SerializeField]
    private float _waitOneSecond;
    [SerializeField]
    private float _timerDelay;

    private void Start()
    {
        _timerText = GameObject.Find("TimerText").GetComponent<TMP_Text>();
        _timerMins = 1;
        _timerSecs = 31;
        _waitOneSecond = 1;
        _timerDelay = -1;
    }

    private void Update()
    {
        TimeText();
    }

    private void TimeText()
    {
        if (Time.time > _timerDelay)
        {
            if (_timerSecs <= 0)
            {
                _timerMins = 0;
                _timerSecs = 60;
            }
            else if (_timerSecs > 0)
            {
                _timerSecs--;
                _timerDelay = Time.time + _waitOneSecond;
            }
        }

        if (_timerSecs >= 10)
        {
            _timerText.text = _timerMins.ToString() + ":" + _timerSecs.ToString();
        }
        else if (_timerSecs < 10)
        {
            _timerText.text = _timerMins.ToString() + ":0" + _timerSecs.ToString();
        }
    }
}
