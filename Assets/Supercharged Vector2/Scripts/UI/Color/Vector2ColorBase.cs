using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.SuperchargedVector2.UI
{
    public abstract class Vector2ColorBase : Vector2Source
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
                if (displayType == "X")
                {
                    color = colorOnDurationReached;
                }

                if (displayType == "DifferenceYX")
                {
                    color = colorOnDurationReached;
                }
            }
            else
            {
                if (displayType == "X")
                {
                    color = originalColor;
                }

                if (displayType == "DifferenceYX")
                {
                    color = originalColor;
                }
            }

        }
    }
}