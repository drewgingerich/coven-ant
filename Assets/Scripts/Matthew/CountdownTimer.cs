using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountdownTimer : MonoBehaviour
{
    public Timer countdownTimer;
    private Text text;
    
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        text = GetComponent<Text>();
        if(!text) {
            Debug.LogError("No text object found!!");
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        text.text = countdownTimer.timeRemaining.ToString("00");
    }
}
