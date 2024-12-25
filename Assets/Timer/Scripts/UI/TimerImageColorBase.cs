using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public abstract class TimerImageColorBase : TimerSource
    {
        // public TimerImage timerImage;

        public bool changeColorOnDurationReached;
        private Color originalColor;
        public Color colorOnDurationReached;

        protected abstract Color GetOriginalColor();

        void Start()
        {
            originalColor = GetOriginalColor();
        }


        protected void SetColor(Image image, TimerSource timer, DisplayType displayType)
        {
            if (changeColorOnDurationReached && timer.GetReference().IsDurationReached())
            {
                if (displayType == DisplayType.Elapsed)
                {
                    image.color = colorOnDurationReached;
                }

                if (displayType == DisplayType.TimeLeft)
                {
                    image.color = colorOnDurationReached;
                }
            }
            else
            {
                if (displayType == DisplayType.Elapsed)
                {
                    image.color = originalColor;
                }

                if (displayType == DisplayType.TimeLeft)
                {
                    image.color = originalColor;
                }
            }
        }
    }
}