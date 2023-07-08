using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
  // public float timeValue = 90;
  // public bool timerOn = false;

  // public TextAlignment TimerTxt;

  // void Start()
  // {
  //   timerOn = true;
  // }

  // void Update()
  // {
  //   if (timerOn)
  //   {
  //     if (timeValue > 0)
  //     {
  //       timeValue -= Time.deltaTime;
  //       updateTimer(timeValue);
  //     }
  //     else
  //     {
  //       Debug.Log("Time is UP!");
  //       timeValue = 0;
  //       timerOn = false;
  //     }
  //   }
  // }

  // void updateTimer(float currentTime)
  // {
  //   float minutes = Mathf.FloorToInt(currentTime / 60);
  //   float seconds = Mathf.FloorToInt(currentTime % 60);

  //   TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
  // }
}
