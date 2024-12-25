using UnityEngine;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerTextBase : MonoBehaviour
    {
        [Header("Configuration")]
        public string format = "0.##";

        public enum DisplayType { Duration, ElapsedTime, TimeLeft }

        public DisplayType displayType;

        public bool clampTextToBounds = false;
        public float minTextBounds = 0f;

        [Header("References")]

        public TimerSource timer;


        protected void SetText(ref string text, DisplayType displayType, string format, bool clampTextToBounds, float minTextBounds)
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

            SetText(ref text, value, format);
        }

        protected void SetText(ref string text, float value, string format)
        {
            text = value.ToString(format);
        }
    }
}