using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public SelectableNavigator navigator;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            navigator.Use();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            navigator.MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            navigator.MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            navigator.MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            navigator.MoveDown();
        }
    }

}
