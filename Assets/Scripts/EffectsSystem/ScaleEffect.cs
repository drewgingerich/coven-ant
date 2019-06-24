using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Scaling")]
public class ScaleEffect : TransformEffect
{
    public Vector2 scale = Vector2.one;

    protected override void SetAction(FeatureController feature)
    {
        feature.SetScale(scale);
    }

    protected override void AddAction(FeatureController feature)
    {
        feature.AddScale(scale);
    }

    protected override void MultiplyAction(FeatureController feature)
    {
        feature.MultiplyScale(scale);
    }
}
