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
        public Timer timer;
        public Image image;
        public TMP_Text text;

        [Header("Configuration")]
        public ImageDisplayType imageDisplayType;
        public TextDisplayType textDisplayType;

        public string textFormat = "F0";

        void Update()
        {
            if (imageDisplayType == ImageDisplayType.Elapsed)
            {
                image.fillAmount = timer.elapsedTime / timer.duration;
            }

            if (imageDisplayType == ImageDisplayType.TimeLeft)
            {
                image.fillAmount = timer.GetTimeLeft() / timer.duration;
            }

            if (text != null)
            {
                SetText(text, textDisplayType, textFormat);
            }
        }

        void SetText(TMP_Text text, TextDisplayType displayType, string format)
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