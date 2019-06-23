using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Composite")]
public class CompositeEffect : ScriptableObject
{
    public List<Effect> effects;

    public void Apply()
    {
        foreach (Effect effect in effects)
        {
            effect.Apply();
        }
    }
}
