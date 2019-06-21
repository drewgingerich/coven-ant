using UnityEngine;

public class ArcadeController : MonoBehaviour
{
    public SelectableNavigator navigator;

    private void Update() {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetButton("Submit"))
        {
            navigator.Use();
        }

        if ( move.x == 1)
        {
            navigator.MoveRight();
        }
        else if (move.x == -1)
        {
            navigator.MoveLeft();
        }
        else if(move.y == 1)
        {
            navigator.MoveUp();
        }
        else if (move.y == -1)
        {
            navigator.MoveDown();
        }
    }
}
