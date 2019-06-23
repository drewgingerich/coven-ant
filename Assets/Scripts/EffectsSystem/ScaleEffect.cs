using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Scaling")]
public class ScaleEffect : AdditiveEffect
{
    public Vector2 scale = Vector2.one;

    protected override void AbsoluteAction(FeatureController feature)
    {
        feature.SetScale(scale);
    }

    protected override void AdditiveAction(FeatureController feature)
    {
        feature.Scale(scale);
    }
}
