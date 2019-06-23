using UnityEngine;

public class DestroyOnCommand : MonoBehaviour
{
    public bool destroyOnStart;
    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if( destroyOnStart ) {
            SelfDestruct();
        }
    } 
}
