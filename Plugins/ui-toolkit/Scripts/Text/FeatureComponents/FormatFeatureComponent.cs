using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public class FormatFeatureComponent : BaseTextFeatureComponent
    {
        public string format = "#,##0.00";

        public override float ProcessValue(float value, float max, ref float minValue, ref float maxValue)
        {
            // This component doesn't modify the value, only affects text formatting
            return value;
        }
    }
}

