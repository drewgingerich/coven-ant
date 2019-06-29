using UnityEngine;

[CreateAssetMenu(menuName = "Effects/ColorAddRandom")]
public class ColorAddRandomEffect : Effect
{
    protected override void Action(FeatureController feature)
    {
        var color = new Color(
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f), 
            Random.Range(0f, 1f)
        );

        feature.AddColor(color);
    }
}
