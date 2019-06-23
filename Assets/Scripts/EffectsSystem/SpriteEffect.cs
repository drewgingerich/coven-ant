using UnityEngine;

[CreateAssetMenu(menuName = "Effects/SpriteSwap")]
public class SpriteEffect : Effect
{
    public Sprite sprite = null;

    protected override void Action(FeatureController feature)
    {
        feature.SetSprite(sprite);
    }
}