using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Translation")]
public class TranslationEffect : TransformEffect
{
    public Vector2 translation = Vector2.one;

    protected override void SetAction(FeatureController feature)
    {
        feature.SetTranslation(translation);
    }

    protected override void AddAction(FeatureController feature)
    {
        feature.AddTranslation(translation);
    }
    protected override void MultiplyAction(FeatureController feature)
    {
        feature.MultiplyTranslation(translation);
    }
}
