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
        public DisplayType2 displayType;

        [Header("References")]

        public TimerSource timer;

        protected void SetText(ref string text, DisplayType2 displayType, string format, bool clampTextToBounds, float minTextBounds)
        {
            var value = 0f;

            bool isShowingNumber = true;

            if (displayType.value == DisplayType2.Options.Duration)
            {
                value = timer.GetReference().data.duration;
            }

            if (displayType.value == DisplayType2.Options.ElapsedTime)
            {
                value = timer.GetReference().data.elapsedTime;
                if (hideTextOnDurationReached && value >= timer.GetReference().data.duration)
                {
                    isShowingNumber = false;
                }
            }

            if (displayType.value == DisplayType2.Options.TimeLeft)
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
                    value = Mathf.Clamp(value, minTextBounds, timer.GetReference().data.duration);

                text = value.ToString(format);
            }
            else
                text = "";
        }
    }
}