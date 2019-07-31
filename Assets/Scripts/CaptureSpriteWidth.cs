using UnityEngine;

[ExecuteInEditMode]
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

    private void Update()
    {
        var targetPosition= spriteRenderer.sprite.bounds.center;
        targetPosition.z = transform.position.z;
        transform.position = targetPosition;

        var spriteScale = spriteRenderer.transform.lossyScale;
        var spriteHalfWidth = spriteRenderer.sprite.bounds.extents.x;
        spriteHalfWidth *= spriteScale.x;
        camera.orthographicSize = spriteHalfWidth / camera.aspect;
    }
}
