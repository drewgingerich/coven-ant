using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    public UnityEvent OnUse;

    public Selectable left;
    public Selectable right;
    public Selectable up;
    public Selectable down;

    public void Select()
    {

    }

    public void Deselect()
    {

    }

    public void Use()
    {
        OnUse.Invoke();
    }
}
