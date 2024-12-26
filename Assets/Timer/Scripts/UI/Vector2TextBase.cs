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

        public bool hideTextOnDurationReached;
        public DisplayType displayType;

        [Header("References")]

        public Vector2Source timer;

        protected void SetText(ref string text, DisplayType displayType, string format, bool clampTextToBounds, float minTextBounds)
        {
            var value = 0f;

            bool isShowingNumber = true;

            if (displayType.value == "Duration")
            {
                value = timer.GetReference().Y;
            }

            if (displayType.value == "ElapsedTime")
            {
                value = timer.GetReference().X;
                if (hideTextOnDurationReached && value >= timer.GetReference().Y)
                {
                    isShowingNumber = false;
                }
            }

            if (displayType.value == "TimeLeft")
            {
                value = timer.GetReference().GetDifferenceYX();
                if (hideTextOnDurationReached && value <= 0)
                {
                    isShowingNumber = false;
                }
            }

            if (isShowingNumber)
            {
                if (clampTextToBounds)
                    value = Mathf.Clamp(value, minTextBounds, timer.GetReference().Y);

                text = value.ToString(format);
            }
            else
                text = "";
        }
    }
}