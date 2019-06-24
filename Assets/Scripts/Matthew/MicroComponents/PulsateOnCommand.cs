using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PulsateOnCommand : MonoBehaviour
{
    
    public bool startPulsating = false;
    private bool pulsating = false;
    public Vector3 rotationalRange;
    private float currentIntensity = 1.0f;
    public float speed = 2f;
    [Range(0,1)]
    public float accelerationPercentage = 0.5f;
    public AnimationCurve oneshotAnimationCurve;
    private Vector3 relativeRotation;
    private float runTime = 0.0f;
    private Vector3 target;
    private bool invertDirection = false;
    private Quaternion originalRotation;

     public void StartPulsating() {
        if(!pulsating) {
            pulsating = true;
            invertDirection = Random.Range(0,1) == 1 ? true : false;
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


    public void PulsateOneshot(float duration ) {
        StopAllCoroutines();
        StartCoroutine(PulsateOneshotRoutine(duration));
    }

    IEnumerator PulsateOneshotRoutine(float duration ) {
        StartPulsating();
        for( float time = 0.0f; time < duration; time += Time.deltaTime ) {
            currentIntensity = oneshotAnimationCurve.Evaluate(time/duration);
            yield return null;
        }
        StopPulsating();
        currentIntensity = 1.0f;
    }
    
    void Start() {
        currentIntensity = 1.0f;
        if( startPulsating ) {
            StartPulsating();
        }
    }


    void Update() {
        if(pulsating) {
            for( int i = 0; i < 3; i++ ) {
                target[i] = rotationalRange[i] * Mathf.Sin(Time.time * speed) * currentIntensity;
                if( invertDirection ) {
                    target[i] = -target[i];
                }
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
