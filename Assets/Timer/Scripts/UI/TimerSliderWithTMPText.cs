using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSliderWithTMPText : MonoBehaviour
    {
        public enum TextDisplayType { Duration, ElapsedTime, TimeLeft }

        public enum SliderDisplayType
        {
            Elapsed,
            TimeLeft
        }

        public SliderDisplayType sliderDisplayType;
        public TextDisplayType leftTextDisplayType;
        public TextDisplayType rightTextDisplayType;

        public TMP_Text leftText;
        public TMP_Text rightText;

        public Timer timer;
        public Slider slider;

        void Update()
        {
            if (sliderDisplayType == SliderDisplayType.Elapsed)
            {
                slider.value = timer.elapsedTime;
                slider.maxValue = timer.duration;
            }

            if (sliderDisplayType == SliderDisplayType.TimeLeft)
            {
                slider.value = timer.GetTimeLeft();
                slider.maxValue = timer.duration;
            }

            SetText(leftText, leftTextDisplayType);
            SetText(rightText, rightTextDisplayType);
        }

        void SetText(TMP_Text text, TextDisplayType displayType)
        {
            if (displayType == TextDisplayType.Duration)
            {
                text.text = timer.duration.ToString();
            }

            if (displayType == TextDisplayType.ElapsedTime)
            {
                text.text = timer.elapsedTime.ToString();
            }

            if (displayType == TextDisplayType.TimeLeft)
            {
                text.text = timer.GetTimeLeft().ToString();
            }
        }
    }
}
