﻿using UnityEngine;
using UnityEngine.Events;

public class PulsateOnCommand : MonoBehaviour
{
    
    public bool startPulsating = false;
    private bool pulsating = false;
   
    public Vector3 rotationalRange;
    public float speed = 2f;
    [Range(0,1)]
    public float accelerationPercentage = 0.5f;
    private Vector3 relativeRotation;
    private float runTime = 0.0f;
    private Vector3 target;
    private Quaternion originalRotation;

     public void StartPulsating() {
        if(!pulsating) {
            pulsating = true;
            originalRotation = transform.rotation;
            relativeRotation = new Vector3(0,0,0);
            runTime = 0.0f;
        }
     }

     public void StopPulsating() {
         if(pulsating) {
            pulsating = false;
            transform.rotation = originalRotation;
            target = Vector3.zero;
        }
     }
    
    void Start() {
        if( startPulsating ) {
            StartPulsating();
        }
    }

    void Update() {
        if(pulsating) {
            for( int i = 0; i < 3; i++ ) {
                target[i] = rotationalRange[i] * Mathf.Sin(Time.time * speed);
            }
        }
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(target), accelerationPercentage);
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable() {
        StopPulsating();
    }
}