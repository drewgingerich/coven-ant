using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ColorChange")]
public class ColorEffect : AdditiveEffect
{
    public Color color = Color.white;

    protected override void AbsoluteAction(FeatureController feature)
    {
        feature.SetColor(color);
    }

    protected override void AdditiveAction(FeatureController feature)
    {
        feature.ShiftColor(color);
    }
}
