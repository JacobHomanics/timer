using System;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public abstract class FlashingFeatureComponent : BaseCurrentMaxComponent
    {
        public Color flashColor1 = Color.red;
        public Color flashColor2 = Color.white;
        public float flashSpeed = 15f;

        public static Color CalcColor(float flashSpeed, Color flashColor1, Color flashColor2)
        {
            float flashValue = Mathf.Sin(Time.time * flashSpeed) * 0.5f + 0.5f;
            Color flashColor = Color.Lerp(flashColor1, flashColor2, flashValue);
            return flashColor;
        }

        public float Normalize(float value1, float value2)
        {
            return value1 / value2;
        }
    }
}

