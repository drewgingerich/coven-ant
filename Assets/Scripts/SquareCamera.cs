using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class SquareCamera : MonoBehaviour
{
    private Camera _camera;
    private new Camera camera
    {
        get
        {
            if (_camera == null)
            {
                _camera = GetComponent<Camera>();
            }
            return _camera;
        }
    } 

    private void Update()
    {
        // Use screen aspect ratio since we're messing with the camera's
        var aspect = (float)Screen.width / (float)Screen.height;
        camera.rect = new Rect(0, 0, 1 / aspect, 1);
    }
}
