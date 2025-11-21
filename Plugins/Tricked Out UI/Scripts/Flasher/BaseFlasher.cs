using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public abstract class BaseFlasher : MonoBehaviour
    {
        public Color flashColor1 = Color.red;
        public Color flashColor2 = Color.white;
        public float flashDuration = 0.4f;

        public static Color CalcColor(float flashDuration, Color flashColor1, Color flashColor2)
        {
            float flashValue = Mathf.Sin(Time.time * Mathf.PI * 2 / flashDuration) * 0.5f + 0.5f;
            Color flashColor = Color.Lerp(flashColor1, flashColor2, flashValue);
            return flashColor;
        }

        public float Normalize(float value1, float value2)
        {
            return value1 / value2;
        }
    }
}

