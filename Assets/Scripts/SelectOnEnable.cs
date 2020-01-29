using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Selectable))]
public class SelectOnEnable : MonoBehaviour
{
    UnityEngine.UI.Selectable item;

    private void Awake()
    {
        item = GetComponent<UnityEngine.UI.Selectable>();
    }

    private void OnEnable()
    {
        item.Select();
        item.OnSelect(null);
    }
}
