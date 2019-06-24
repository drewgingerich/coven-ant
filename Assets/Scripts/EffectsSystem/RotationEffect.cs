using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Rotation")]
public class RotateEffect : TransformEffect
{
    public float rotation = 0;

    protected override void SetAction(FeatureController feature)
    {
        feature.SetRotation(rotation);
    }

    protected override void AddAction(FeatureController feature)
    {
        feature.AddRotation(rotation);
    }

    protected override void MultiplyAction(FeatureController feature)
    {
        feature.MultiplyRotation(rotation);
    }
}
