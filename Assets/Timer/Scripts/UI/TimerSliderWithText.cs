using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSliderWithText : MonoBehaviour
    {
        public enum TextDisplayType { Duration, ElapsedTime, TimeLeft }

        public enum SliderDisplayType
        {
            Elapsed,
            TimeLeft
        }

        [Header("References")]
        public Timer timer;
        public Slider slider;

        public Text leftText;
        public Text rightText;

        [Header("Configuration")]
        public SliderDisplayType sliderDisplayType;
        public TextDisplayType leftTextDisplayType;
        public string leftTextFormat = "F3";
        public TextDisplayType rightTextDisplayType;
        public string rightTextFormat = "F3";

        void Update()
        {
            if (slider != null)
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
            }

            if (leftText != null)
                SetText(leftText, leftTextDisplayType, leftTextFormat);
            if (rightText != null)
                SetText(rightText, rightTextDisplayType, rightTextFormat);
        }

        void SetText(Text text, TextDisplayType displayType, string format)
        {
            if (displayType == TextDisplayType.Duration)
            {
                text.text = timer.duration.ToString(format);
            }

            if (displayType == TextDisplayType.ElapsedTime)
            {
                text.text = timer.elapsedTime.ToString(format);
            }

            if (displayType == TextDisplayType.TimeLeft)
            {
                text.text = timer.GetTimeLeft().ToString(format);
            }
        }
    }
}
