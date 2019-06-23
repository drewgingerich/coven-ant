using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Rotation")]
public class RotateEffect : AdditiveEffect
{
    public float rotation = 0;

    protected override void AbsoluteAction(FeatureController feature)
    {
        feature.SetRotation(rotation);
    }

    protected override void AdditiveAction(FeatureController feature)
    {
        feature.Rotate(rotation);
    }
}
