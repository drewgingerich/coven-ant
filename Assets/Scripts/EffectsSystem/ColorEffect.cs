using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ColorChange")]
public class ColorEffect : Effect
{
    public Color color = Color.white;
    
    protected override void Action(FeatureController feature)
    {
        feature.SetColor(color);
    }
}
