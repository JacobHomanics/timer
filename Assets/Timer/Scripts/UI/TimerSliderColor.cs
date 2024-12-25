using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSliderColor : TimerImageColorBase
    {
        public Slider slider;
        public TimerSlider timerSlider;
        public TimerSource timer;

        public DisplayType displayType;


        public override Timer GetReference()
        {
            return timerSlider.timer.GetReference();
        }

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
                SetColor(slider.fillRect.GetComponent<Image>(), timer.GetReference(), displayType.value);
            }

            if (displayType.value == DisplayType.Options.TimeLeft)
            {
                SetColor(slider.transform.Find("Background").GetComponent<Image>(), timer.GetReference(), displayType.value);
            }
        }
    }
}