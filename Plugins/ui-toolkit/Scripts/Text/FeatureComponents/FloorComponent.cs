using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public class FloorComponent : BaseTextFeatureComponent
    {
        public override float ProcessValue(float value, float max, ref float minValue, ref float maxValue)
        {
            return Mathf.Floor(value);
        }
    }
}

