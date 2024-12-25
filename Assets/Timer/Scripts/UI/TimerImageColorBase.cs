using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public abstract class TimerImageColorBase : TimerSource
    {
        public override Timer GetReference()
        {
            return timer.GetReference();
        }

        [Header("Configuration")]
        public bool changeColorOnDurationReached;
        private Color originalColor;
        public Color colorOnDurationReached;
        public DisplayType displayType;

        [Header("References")]
        public TimerSource timer;

        protected abstract Color GetOriginalColor();

        void Start()
        {
            originalColor = GetOriginalColor();
        }

        protected void SetColor(out Color color, TimerSource timer, DisplayType.Options displayType)
        {
            color = Color.white;

            if (changeColorOnDurationReached && timer.GetReference().IsDurationReached())
            {
                if (displayType == DisplayType.Options.Elapsed)
                {
                    color = colorOnDurationReached;
                }

                if (displayType == DisplayType.Options.TimeLeft)
                {
                    color = colorOnDurationReached;
                }
            }
            else
            {
                if (displayType == DisplayType.Options.Elapsed)
                {
                    color = originalColor;
                }

                if (displayType == DisplayType.Options.TimeLeft)
                {
                    color = originalColor;
                }
            }

        }
    }
}