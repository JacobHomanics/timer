using System;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public abstract class BaseEnabledThreshold : BaseVector2Component
    {
        [Range(0f, 100f)]
        public float thresholdPercent = 20f;

        public enum ThresholdType
        {
            below, above
        }

        public ThresholdType thresholdType;

        public bool IsEnabled()
        {
            return IsEnabled(thresholdType, X, Y, thresholdPercent);
        }

        public static bool IsEnabled(ThresholdType thresholdType, float current, float max, float thresholdPercent)
        {
            float healthPercent;
            healthPercent = current / max;

            bool condition = false;
            if (thresholdType == ThresholdType.below)
                condition = healthPercent <= (thresholdPercent / 100f);
            if (thresholdType == ThresholdType.above)
                condition = healthPercent >= (thresholdPercent / 100f);

            return condition;
        }
    }
}

