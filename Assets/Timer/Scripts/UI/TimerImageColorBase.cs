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

        // public TMPro.TMP_Text text;

        protected void SetColor(Image image, TimerSource timer, DisplayType.Options displayType)
        {
            if (changeColorOnDurationReached && timer.GetReference().IsDurationReached())
            {
                if (displayType == DisplayType.Options.Elapsed)
                {
                    // text.color = colorOnDurationReached;
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