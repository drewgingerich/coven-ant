using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public FeatureLink target;

    public void Apply()
    {
        Action(target.feature);
    }

    protected abstract void Action(FeatureController feature);
}
