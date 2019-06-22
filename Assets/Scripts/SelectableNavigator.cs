using UnityEngine;

public class SelectableNavigator : MonoBehaviour
{
    public Selectable selected;

    public void Use()
    {
        selected.Use();
    }

    public void MoveLeft()
    {
        ChangeSelection(selected.left);
    }

    public void MoveRight()
    {
        ChangeSelection(selected.right);
    }

    public void MoveUp()
    {
        ChangeSelection(selected.up);
    }

    public void MoveDown()
    {
        ChangeSelection(selected.down);
    }

    private void ChangeSelection(Selectable next)
    {
        if (next != null)
        {
            selected.Deselect();
            selected = next;
            selected.Select();
        }
    }

    void Start()
    {
        if(selected) {
            selected.Select();
        }
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        if(selected) {
            selected.Deselect();
        }
    }
}
