using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public abstract class TimerImageColorBase : TimerSource
    {
        public bool changeColorOnDurationReached;
        private Color originalColor;
        public Color colorOnDurationReached;

        protected abstract Color GetOriginalColor();

        void Start()
        {
            originalColor = GetOriginalColor();
        }

        protected void SetColor(Image image, TimerSource timer, DisplayType.Options displayType)
        {
            if (changeColorOnDurationReached && timer.GetReference().IsDurationReached())
            {
                if (displayType == DisplayType.Options.Elapsed)
                {
                    image.color = colorOnDurationReached;
                }

                if (displayType == DisplayType.Options.TimeLeft)
                {
                    image.color = colorOnDurationReached;
                }
            }
            else
            {
                if (displayType == DisplayType.Options.Elapsed)
                {
                    image.color = originalColor;
                }

                if (displayType == DisplayType.Options.TimeLeft)
                {
                    image.color = originalColor;
                }
            }
        }
    }
}