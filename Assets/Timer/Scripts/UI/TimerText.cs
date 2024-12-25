using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerText : MonoBehaviour
    {
        public TimerBase timer;

        public Text text;

        public string format = "F3";

        public enum DisplayType { Duration, ElapsedTime, TimeLeft }

        public DisplayType displayType;

        public bool clampTextToBounds = false;
        public float minTextBounds = 0f;

        public void Update()
        {
            SetText(text, displayType, format, clampTextToBounds, minTextBounds);
        }

        void SetText(Text text, DisplayType displayType, string format, bool clampTextToBounds, float minTextBounds)
        {
            var value = 0f;

            if (displayType == DisplayType.Duration)
            {
                value = timer.GetReference().data.duration;
            }

            if (displayType == DisplayType.ElapsedTime)
            {
                value = timer.GetReference().data.elapsedTime;
            }

            if (displayType == DisplayType.TimeLeft)
            {
                value = timer.GetReference().GetTimeLeft();
            }

            if (clampTextToBounds)
                value = Mathf.Clamp(value, minTextBounds, timer.GetReference().data.duration);

            SetText(text, value, format);
        }

        void SetText(Text text, float value, string format)
        {
            text.text = value.ToString(format);
        }
    }
}