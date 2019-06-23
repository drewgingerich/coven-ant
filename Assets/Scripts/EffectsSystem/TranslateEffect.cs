using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Translation")]
public class TranslationEffect : AdditiveEffect
{
    public Vector2 translation = Vector2.one;

    protected override void AbsoluteAction(FeatureController feature)
    {
        feature.SetTranslation(translation);
    }

    protected override void AdditiveAction(FeatureController feature)
    {
        feature.Translate(translation);
    }
}
