using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.PlayerLoop;

public class TimerController : MonoBehaviour 
{
    public Text TimerTxt;
    public float PlayTime;
    public bool TimerOn = false;
    
    private void Start()
    {
        TimerOn = true;
    }

    private void Update()
    {
        if (TimerOn)
        {
            UpdateTimer(PlayTime);
            PlayTime += Time.deltaTime;
        }
    }

  private  void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        TimerTxt.text = "Time: " +  string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}