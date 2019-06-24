using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ColorAdd")]
public class ColorAddEffect : Effect
{
    public Color color = Color.white;
    
    protected override void Action(FeatureController feature)
    {
        feature.AddColor(color);
    }
}
