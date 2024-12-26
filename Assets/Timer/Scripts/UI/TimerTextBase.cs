using UnityEngine;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerTextBase : TimerSource
    {
        public override Timer GetReference()
        {
            return timer.GetReference();
        }

        [Header("Configuration")]
        public string format = "0.##";

        public bool clampTextToBounds = false;
        public float minTextBounds = 0f;

        public bool hideTextOnDurationReached;
        public DisplayType displayType;

        [Header("References")]

        public TimerSource timer;

        protected void SetText(ref string text, DisplayType displayType, string format, bool clampTextToBounds, float minTextBounds)
        {
            var value = 0f;

            bool isShowingNumber = true;

            if (displayType.value == "Duration")
            {
                value = timer.GetReference().Duration;
            }

            if (displayType.value == "ElapsedTime")
            {
                value = timer.GetReference().ElapsedTime;
                if (hideTextOnDurationReached && value >= timer.GetReference().Duration)
                {
                    isShowingNumber = false;
                }
            }

            if (displayType.value == "TimeLeft")
            {
                value = timer.GetReference().GetTimeLeft();
                if (hideTextOnDurationReached && value <= 0)
                {
                    isShowingNumber = false;
                }
            }

            if (isShowingNumber)
            {
                if (clampTextToBounds)
                    value = Mathf.Clamp(value, minTextBounds, timer.GetReference().Duration);

                text = value.ToString(format);
            }
            else
                text = "";
        }
    }
}