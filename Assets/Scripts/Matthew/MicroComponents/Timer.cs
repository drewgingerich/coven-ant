using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{

    public bool beginTimerOnStart = false;
    public float timeRemaining;
    private float originalTimeRemaining;
    public bool repeatTimer = false;
    public UnityEvent OnTimerStart;
    public UnityEvent OnTimerPause;
    public UnityEvent OnTimerEnd;
    private bool timerRunning;

    public void BeginTimer()
    {
        timerRunning = true;
        OnTimerStart.Invoke();
        if( timeRemaining <= 0 ) {
            timeRemaining = originalTimeRemaining;
        }
    }

    public void BeginTimer(float newDuration)
    {
        timeRemaining = newDuration;
        originalTimeRemaining = newDuration;
        BeginTimer();
    }

    public void PauseTimer()
    {
        timerRunning = false;
        OnTimerPause.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        originalTimeRemaining = timeRemaining;
        if(beginTimerOnStart) {
            BeginTimer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if( timerRunning ) {
            timeRemaining -= Time.deltaTime;
            if( timeRemaining < 0 ) {
                timeRemaining = 0;
                timerRunning = false;
                OnTimerEnd.Invoke();
                if(repeatTimer) {
                    BeginTimer();
                }
            }
        }
    }
}
