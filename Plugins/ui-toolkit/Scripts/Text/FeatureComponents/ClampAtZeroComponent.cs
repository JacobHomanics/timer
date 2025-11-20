namespace JacobHomanics.TrickedOutUI
{
    public class ClampAtZeroComponent : BaseTextFeatureComponent
    {
        public override float ProcessValue(float value, float max, ref float minValue, ref float maxValue)
        {
            minValue = 0;
            return value;
        }
    }
}

