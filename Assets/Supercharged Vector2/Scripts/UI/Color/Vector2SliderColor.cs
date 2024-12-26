using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.SuperchargedVector2.UI
{
    public class Vector2SliderColor : Vector2ColorBase
    {
        public Slider slider;

        protected override Color GetOriginalColor()
        {
            if (displayType.value == "X")
            {
                return slider.fillRect.GetComponent<Image>().color;
            }

            if (displayType.value == "DifferenceYX")
            {
                return slider.transform.Find("Background").GetComponent<Image>().color;
            }

            return Color.white;
        }

        void Update()
        {
            if (displayType.value == "X")
            {
                SetColor(out Color color, timer.GetReference(), displayType.value);
                slider.fillRect.GetComponent<Image>().color = color;
            }

            if (displayType.value == "DifferenceYX")
            {
                SetColor(out Color color, timer.GetReference(), displayType.value);
                slider.transform.Find("Background").GetComponent<Image>().color = color;
            }
        }
    }
}