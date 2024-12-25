using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImageWithTMPText : MonoBehaviour
    {
        public enum ImageDisplayType
        {
            Elapsed,
            TimeLeft
        }

        public enum TextDisplayType { Duration, ElapsedTime, TimeLeft }

        [Header("References")]
        public TimerSource timer;
        public Image image;
        public TMP_Text text;

        [Header("Configuration")]
        public ImageDisplayType imageDisplayType;
        public TextDisplayType textDisplayType;

        public string textFormat = "F0";

        public bool clampTextToBounds = false;
        public float minTextBounds = 0f;

        void Update()
        {
            if (imageDisplayType == ImageDisplayType.Elapsed)
            {
                image.fillAmount = timer.GetReference().data.elapsedTime / timer.GetReference().data.duration;
            }

            if (imageDisplayType == ImageDisplayType.TimeLeft)
            {
                image.fillAmount = timer.GetReference().GetTimeLeft() / timer.GetReference().data.duration;
            }

            if (text != null)
            {
                SetText(text, textDisplayType, textFormat, clampTextToBounds, minTextBounds);
            }
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