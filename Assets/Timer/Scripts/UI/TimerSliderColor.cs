using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSliderColor : TimerImageColorBase
    {
        public TimerSlider timerSlider;

        public override Timer GetReference()
        {
            return timerSlider.timer.GetReference();
        }

        protected override Color GetOriginalColor()
        {
            if (timerSlider.displayType == DisplayType.Elapsed)
            {
                return timerSlider.slider.fillRect.GetComponent<Image>().color;
            }

            if (timerSlider.displayType == DisplayType.TimeLeft)
            {
                return timerSlider.slider.transform.Find("Background").GetComponent<Image>().color;
            }

            return Color.white;
        }

        void Update()
        {
            if (timerSlider.displayType == DisplayType.Elapsed)
            {
                SetColor(timerSlider.slider.fillRect.GetComponent<Image>(), GetReference(), timerSlider.displayType);
            }

            if (timerSlider.displayType == DisplayType.TimeLeft)
            {
                SetColor(timerSlider.slider.transform.Find("Background").GetComponent<Image>(), GetReference(), timerSlider.displayType);
            }
        }
    }
}