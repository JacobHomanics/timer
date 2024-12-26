using UnityEngine;

namespace JacobHomanics.Core.Timer.UI
{
    public class Vector2TextBase : Vector2Source
    {
        public override Vector2 GetReference()
        {
            return timer.GetReference();
        }

        [Header("Configuration")]
        public string format = "0.##";

        public bool clampTextToBounds = false;
        public float minTextBounds = 0f;

        public DisplayType displayType;

        [Header("References")]

        public Vector2Source timer;

        protected void SetText(ref string text, DisplayType displayType, string format, bool clampTextToBounds, float minTextBounds)
        {
            var value = 0f;

            if (displayType.value == "Y")
            {
                value = timer.GetReference().Y;
            }

            if (displayType.value == "X")
            {
                value = timer.GetReference().X;

            }

            if (displayType.value == "DifferenceYX")
            {
                value = timer.GetReference().GetDifferenceYX();

            }

            text = value.ToString(format);
        }
    }
}