using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public abstract class BaseTextFeatureComponent : MonoBehaviour
    {
        public abstract float ProcessValue(float value, float max, ref float minValue, ref float maxValue);
    }
}

