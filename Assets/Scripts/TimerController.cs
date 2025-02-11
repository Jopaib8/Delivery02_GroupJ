using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class TimerController : MonoBehaviour 
{
    public static TimerController instance;
    public Text timeCounter;
    private TimeSpan timePlaying;
    private bool isTimerGoing;

    private float passedTime;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        timeCounter.text = "Time: 00:00:00";
        isTimerGoing = false;
    }

    public void BeginTimer()
    {
        isTimerGoing = true;
        
        passedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTImer()
    {
        isTimerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while(isTimerGoing)
        {
            passedTime += Time.deltaTime;

            timePlaying = TimeSpan.FromSeconds(passedTime);

            string timePlayingStr = "Time" + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
            yield return null;
        }
    }


}

