using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    public UnityEvent OnUse;
    public bool isHovered;
    public UnityEvent OnHover;
    public UnityEvent OnDeHover;

    public Selectable left;
    public Selectable right;
    public Selectable up;
    public Selectable down;

    public void Select()
    {
        isHovered = true;
        OnHover.Invoke();
    }

    public void Deselect()
    {
        isHovered = false;
        OnDeHover.Invoke();
    }

    public void Use()
    {
        OnUse.Invoke();
    }
}
