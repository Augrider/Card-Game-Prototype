namespace Developed.TweenSystem
{
    public readonly struct TweenRecord
    {
        public readonly ITween Tween;
        public readonly bool UseParametersFromTween;

        public TweenRecord(ITween tween, bool useParametersFromTween)
        {
            Tween = tween;
            UseParametersFromTween = useParametersFromTween;
        }
    }
}