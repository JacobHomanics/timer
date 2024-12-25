using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSliderWithTMPText : MonoBehaviour
    {
        public enum SliderDisplayType
        {
            Elapsed,
            TimeLeft
        }

        public enum TextDisplayType { Duration, ElapsedTime, TimeLeft }

        [Header("References")]
        public TimerSource timer;
        public Slider slider;

        public TMP_Text leftText;
        public TMP_Text rightText;

        [Header("Configuration")]
        public SliderDisplayType sliderDisplayType;
        public TextDisplayType leftTextDisplayType;
        public string leftTextFormat = "F3";
        public bool clampLeftTextToBounds = false;
        public float minLeftTextBounds = 0f;
        public TextDisplayType rightTextDisplayType;
        public string rightTextFormat = "F3";
        public bool clampRightTextToBounds = false;
        public float minRightTextBounds = 0f;

        void Update()
        {
            if (slider != null)
            {

                if (sliderDisplayType == SliderDisplayType.Elapsed)
                {
                    slider.value = timer.GetReference().data.elapsedTime;
                    slider.maxValue = timer.GetReference().data.duration;
                }

                if (sliderDisplayType == SliderDisplayType.TimeLeft)
                {
                    slider.value = timer.GetReference().GetTimeLeft();
                    slider.maxValue = timer.GetReference().data.duration;
                }
            }

            if (leftText != null)
                SetText(leftText, leftTextDisplayType, leftTextFormat, clampLeftTextToBounds, minLeftTextBounds);
            if (rightText != null)
                SetText(rightText, rightTextDisplayType, rightTextFormat, clampRightTextToBounds, minRightTextBounds);
        }

        void SetText(TMP_Text text, TextDisplayType displayType, string format, bool clampTextToBounds, float minTextBounds)
        {
            var value = 0f;

            if (displayType == TextDisplayType.Duration)
            {
                value = timer.GetReference().data.duration;
            }

            if (displayType == TextDisplayType.ElapsedTime)
            {
                value = timer.GetReference().data.elapsedTime;
            }

            if (displayType == TextDisplayType.TimeLeft)
            {
                value = timer.GetReference().GetTimeLeft();
            }

            if (clampTextToBounds)
                value = Mathf.Clamp(value, minTextBounds, timer.GetReference().data.duration);

            SetText(text, value, format);
        }

        void SetText(TMP_Text text, float value, string format)
        {
            text.text = value.ToString(format);
        }
    }
}
