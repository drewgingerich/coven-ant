public abstract class TransformEffect : Effect
{
    public enum Mode { Set, Add, Multiply }

    public Mode mode = Mode.Multiply;

    protected override void Action(FeatureController feature)
    {
        switch(mode) {
            case Mode.Set:
                SetAction(feature);
                break;
            case Mode.Add:
                AddAction(feature);
                break;
            case Mode.Multiply:
                MultiplyAction(feature);
                break;
        }
    }

    protected abstract void SetAction(FeatureController feature);
    protected abstract void AddAction(FeatureController feature);
    protected abstract void MultiplyAction(FeatureController feature);
}
