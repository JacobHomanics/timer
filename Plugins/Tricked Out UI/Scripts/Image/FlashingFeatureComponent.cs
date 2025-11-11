using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public class FlashingFeatureComponent : BaseCurrentMaxComponent
    {
        public Image flashImage;
        public float thresholdPercent = 0.2f;
        public Color flashColor1 = Color.red;
        public Color flashColor2 = Color.white;
        public float flashSpeed = 15f;

        public enum ThresholdType
        {
            below, above
        }

        public ThresholdType thresholdType;

        void Update()
        {
            FlashingFeatureCommand(flashImage, thresholdPercent, flashColor1, flashColor2, flashSpeed, Current, Max, thresholdType);
        }

        public static void FlashingFeatureCommand(Image image, float thresholdPercent, Color flashColor1, Color flashColor2, float flashSpeed, float current, float max, ThresholdType thresholdType)
        {
            float healthPercent;
            healthPercent = current / max;

            bool condition = false;
            if (thresholdType == ThresholdType.below)
                condition = healthPercent <= thresholdPercent;
            if (thresholdType == ThresholdType.above)
                condition = healthPercent >= thresholdPercent;

            image.enabled = condition;

            float flashValue = Mathf.Sin(Time.time * flashSpeed) * 0.5f + 0.5f;
            Color flashColor = Color.Lerp(flashColor1, flashColor2, flashValue);
            image.color = flashColor;
            image.fillAmount = current / max;
        }

    }
}

