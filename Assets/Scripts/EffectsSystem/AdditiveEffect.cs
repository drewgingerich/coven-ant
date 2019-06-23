public abstract class AdditiveEffect : Effect
{
    public bool additive;

    protected override void Action(FeatureController feature)
    {
        if (additive)
        {
            AdditiveAction(feature);
        }
        else
        {
            AbsoluteAction(feature);
        }
    }

    protected abstract void AdditiveAction(FeatureController feature);
    protected abstract void AbsoluteAction(FeatureController feature);

}
