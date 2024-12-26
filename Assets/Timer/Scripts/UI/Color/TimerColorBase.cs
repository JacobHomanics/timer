using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public abstract class TimerColorBase : Vector2Source
    {
        public override Vector2 GetReference()
        {
            return timer.GetReference();
        }

        [Header("Configuration")]
        public Color colorOnDurationReached;
        public DisplayType displayType;

        [Header("References")]
        public Vector2Source timer;

        private Color originalColor;

        protected abstract Color GetOriginalColor();

        void Start()
        {
            originalColor = GetOriginalColor();
        }

        protected void SetColor(out Color color, Vector2Source timer, string displayType)
        {
            color = Color.white;

            if (timer.GetReference().IsXGreatherThanOrEqualToY())
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