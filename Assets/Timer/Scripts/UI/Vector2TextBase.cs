using UnityEngine;

namespace JacobHomanics.Core.Timer.UI
{
    public class Vector2TextBase : Vector2Source
    {
        [Header("Configuration")]
        public string format = "";

        public bool clampTextToBounds = false;
        public float minTextBounds = 0f;

        public DisplayType displayType;

        [Header("References")]

        public Vector2Source vector2;

        public override Vector2 GetReference()
        {
            return vector2.GetReference();
        }

        protected void SetText(ref string text, DisplayType displayType, string format, bool clampTextToBounds, float minTextBounds)
        {
            var value = 0f;

            bool isShowingNumber = true;

            if (displayType.value == "Y")
            {
                value = vector2.GetReference().Y;
            }
            else if (displayType.value == "X")
            {
                value = vector2.GetReference().X;
            }

            else if (displayType.value == "YXDifference")
            {
                value = vector2.GetReference().GetDifferenceYX();
            }

            if (isShowingNumber)
            {
                if (clampTextToBounds)
                    value = Mathf.Clamp(value, minTextBounds, vector2.GetReference().Y);

                text = value.ToString(format);
            }
            else
                text = "";
        }
    }
}