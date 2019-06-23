using UnityEngine;
using UnityEngine.Events;

public class ScriptableUpdateHook : MonoBehaviour
{
    public UnityEvent OnUpdate;
    void Update()
    {
        OnUpdate.Invoke();
    }
}
