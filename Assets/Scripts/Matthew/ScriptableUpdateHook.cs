using UnityEngine;
using UnityEngine.Events;

public class ScriptableUpdateHook : MonoBehaviour
{
    public UnityEvent OnUpdate;

    public UnityEvent Enable;
    public UnityEvent Disable;
    public UnityEvent OnStart;
    void Update()
    {
        OnUpdate.Invoke();
    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        Enable.Invoke();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        Disable.Invoke();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        OnStart.Invoke();
    }
}
