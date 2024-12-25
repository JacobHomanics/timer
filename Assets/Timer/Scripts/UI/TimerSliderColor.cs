using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSliderColor : TimerImageColorBase
    {
        public Slider slider;

        protected override Color GetOriginalColor()
        {
            if (displayType.value == DisplayType.Options.Elapsed)
            {
                return slider.fillRect.GetComponent<Image>().color;
            }

            if (displayType.value == DisplayType.Options.TimeLeft)
            {
                return slider.transform.Find("Background").GetComponent<Image>().color;
            }

            return Color.white;
        }

        void Update()
        {
            if (displayType.value == DisplayType.Options.Elapsed)
            {
                SetColor(out Color color, timer.GetReference(), displayType.value);
                slider.fillRect.GetComponent<Image>().color = color;
            }

            if (displayType.value == DisplayType.Options.TimeLeft)
            {
                SetColor(out Color color, timer.GetReference(), displayType.value);
                slider.transform.Find("Background").GetComponent<Image>().color = color;
            }
        }
    }
}