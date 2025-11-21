namespace JacobHomanics.TrickedOutUI
{
    public class ClampAtMaxComponent : BaseTextFeatureComponent
    {
        public override float ProcessValue(float value, float max, ref float minValue, ref float maxValue)
        {
            maxValue = max;
            return value;
        }
    }
}

