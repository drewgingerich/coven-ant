using UnityEngine;

[ExecuteAlways]
public class FeatureController : MonoBehaviour
{
    public float rotation = 0;
    public Vector2 translation = Vector2.zero;
    public Vector2 scale = Vector2.one;
    public Color color = Color.white;

    private Transform _featureTransform;
    public Transform featureTransform
    {
        get
        {
            if (_featureTransform == null)
            {
                _featureTransform = GetComponent<Transform>();
            }
            return _featureTransform;
        }
        set
        {
            _featureTransform = value;
        }
    }

    private SpriteRenderer _featureSprite;
    public SpriteRenderer featureSprite
    {
        get
        {
            if (_featureSprite == null)
            {
                _featureSprite = GetComponent<SpriteRenderer>();
            }
            return _featureSprite;
        }
        set
        {
            _featureSprite = value;
        }
    }

    public void OnEnable()
    {
        ApplyTransformations();
    }

#if UNITY_EDITOR
    public void Update()
    {
        ApplyTransformations();
    }
#endif

    public void ApplyTransformations()
    {
        SetRotation(rotation);
        SetTranslation(translation);
        SetScale(scale);
        SetColor(color);
    }

    public void Rotate(float value)
    {
        SetRotation(rotation + value);
    }

    public void SetRotation(float value)
    {
        featureTransform.localRotation = Quaternion.Euler(0, 0, value);
        rotation = value;
    }

    public void Translate(Vector2 value)
    {
        SetTranslation(translation + value);
    }

    public void SetTranslation(Vector2 value)
    {
        featureTransform.localPosition = (Vector3)value;
        translation = value;
    }

    public void Scale(Vector2 value)
    {
        SetScale(scale + value);
    }

    public void SetScale(Vector2 value)
    {
        featureTransform.localScale = new Vector3(value.x, value.y, 1);
        scale = value;
    }

    public void ShiftColor(Color value)
    {
        SetColor(color + value);
    }

    public void SetColor(Color value)
    {
        featureSprite.color = value;
        color = value;
    }

    public void SetSprite(Sprite sprite)
    {
        featureSprite.sprite = sprite;
    }
}
