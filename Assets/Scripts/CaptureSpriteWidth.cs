using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CaptureSpriteWidth : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

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

    const float targetAspectRatio = 16 / 9;

    private void Update()
    {
        var currentPosition = transform.position;
        var targetPosition= spriteRenderer.sprite.bounds.center;
        targetPosition.z = currentPosition.z;
        transform.position = targetPosition;

        var spriteScale = spriteRenderer.transform.lossyScale;
        var spriteHalfWidth = spriteRenderer.sprite.bounds.extents.x;
        spriteHalfWidth *= spriteScale.x;
        camera.orthographicSize = spriteHalfWidth / camera.aspect;
    }
}
