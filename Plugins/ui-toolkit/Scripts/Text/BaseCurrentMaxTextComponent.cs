using UnityEngine;
using TMPro;

namespace JacobHomanics.TrickedOutUI
{
    [System.Serializable]
    public class TextProperties
    {
        public TMP_Text text;

        public enum DisplayType
        {
            Current, Max, Difference
        }

        public DisplayType displayType;
        public string format = "#,##0";

        public bool clampAtMax;
        public bool clampAtZero;
        public bool ceil;

        public bool floor;
    }

    public abstract class BaseCurrentMaxTextComponent : BaseCurrentMaxComponent
    {

        public void Display(TMP_Text text, TextProperties.DisplayType displayType, float current, float max, string format, bool clampAtZero, bool clampAtMax, bool ceil, bool floor)
        {
            if (displayType == TextProperties.DisplayType.Current)
                Display(text, current, max, format, clampAtZero, clampAtMax, ceil, floor);
            if (displayType == TextProperties.DisplayType.Max)
                Display(text, max, max, format, clampAtZero, clampAtMax, ceil, floor);
            if (displayType == TextProperties.DisplayType.Difference)
                Display(text, max - current, max, format, clampAtZero, clampAtMax, ceil, floor);
        }

        public void Display(TMP_Text text, float num, float max, string format, bool clampAtZero, bool clampAtMax, bool ceil, bool floor)
        {
            float minValue = Mathf.NegativeInfinity;
            float maxValue = Mathf.Infinity;

            if (clampAtZero)
                minValue = 0;

            if (clampAtMax)
                maxValue = max;

            float finalValue = Mathf.Clamp(num, minValue, maxValue);

            if (ceil)
            {
                finalValue = Mathf.Ceil(finalValue);
            }

            if (floor)
            {
                finalValue = Mathf.Floor(finalValue);
            }

            text.text = finalValue.ToString(format);
        }
    }
}
