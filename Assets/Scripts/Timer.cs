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

    [SerializeField]
    private bool _isTimerStillGoing;

    [SerializeField]
    private GameObject[] _cards;

    [SerializeField]
    private GameObject[] _store;

    private void Start()
    {
        _timerText = GameObject.Find("TimerText").GetComponent<TMP_Text>();
        _timerMins = 0;
        _timerSecs = 31;
        _waitOneSecond = 1;
        _timerDelay = -1;
        _cards = GameObject.FindGameObjectsWithTag("Card");
        _store = GameObject.FindGameObjectsWithTag("Store Item");
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
                _timerSecs = 0;
                _isTimerStillGoing = false;
                foreach (GameObject C in _cards)
                {
                    //C.GetComponent<DraggableCard>().TimerGoing(_isTimerStillGoing);
                    C.GetComponent<DraggableCard>().OnMouseUp();
                }
                foreach (GameObject SI in _store)
                {
                    SI.GetComponent<Store>().IsTimerGoing(_isTimerStillGoing);
                }
            }
            else if (_timerSecs > 0)
            {
                _timerSecs--;
                _timerDelay = Time.time + _waitOneSecond;
                _isTimerStillGoing = true;
                foreach (GameObject C in _cards)
                {
                    //C.GetComponent<DraggableCard>().TimerGoing(_isTimerStillGoing);
                    C.GetComponent<DraggableCard>().OnMouseDown();
                }
                foreach (GameObject SI in _store)
                {
                    SI.GetComponent<Store>().IsTimerGoing(_isTimerStillGoing);
                }
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
