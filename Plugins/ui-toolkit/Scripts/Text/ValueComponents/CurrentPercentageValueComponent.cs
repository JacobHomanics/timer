namespace JacobHomanics.TrickedOutUI
{
    public class CurrentPercentageValueComponent : BaseValueComponent
    {
        public override float GetValue(float current, float max)
        {
            // Calculate percentage using original current value (matching original behavior)
            return (current / max) * 100f;
        }
    }
}

