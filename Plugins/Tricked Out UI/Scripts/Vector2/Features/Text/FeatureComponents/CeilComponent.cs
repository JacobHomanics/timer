using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public class CeilComponent : BaseTextFeatureComponent
    {
        public override float ProcessValue(float value, float max, ref float minValue, ref float maxValue)
        {
            return Mathf.Ceil(value);
        }
    }
}

