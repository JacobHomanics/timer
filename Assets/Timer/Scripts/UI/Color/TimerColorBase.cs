using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public abstract class TimerColorBase : TimerSource
    {
        public override Timer GetReference()
        {
            return timer.GetReference();
        }

        [Header("Configuration")]
        public Color colorOnDurationReached;
        public DisplayType displayType;

        [Header("References")]
        public TimerSource timer;

        private Color originalColor;

        protected abstract Color GetOriginalColor();

        void Start()
        {
            originalColor = GetOriginalColor();
        }

        protected void SetColor(out Color color, TimerSource timer, string displayType)
        {
            color = Color.white;

            if (timer.GetReference().IsDurationReached())
            {
                if (displayType == "ElapsedTime")
                {
                    color = colorOnDurationReached;
                }

                if (displayType == "TimeLeft")
                {
                    color = colorOnDurationReached;
                }
            }
            else
            {
                if (displayType == "ElapsedTime")
                {
                    color = originalColor;
                }

                if (displayType == "TimeLeft")
                {
                    color = originalColor;
                }
            }

        }
    }
}