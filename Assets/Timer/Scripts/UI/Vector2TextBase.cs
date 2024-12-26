using UnityEngine;
using UnityEngine.Serialization;

namespace JacobHomanics.Core.Timer.UI
{
    public class Vector2TextBase : Vector2Source
    {
        public override Vector2 GetReference()
        {
            return vector2.GetReference();
        }

        [Header("Configuration")]
        public string format = "";
        public bool clampMinToZero;
        public bool clampMaxToY;

        public bool roundUp;

        public DisplayType displayType;

        [Header("References")]

        public Vector2Source vector2;

        protected void SetText(ref string text, DisplayType displayType, string format)
        {
            var value = 0f;

            if (displayType.value == "Y")
            {
                value = vector2.GetReference().Y;
            }

            if (displayType.value == "X")
            {
                value = vector2.GetReference().X;
            }

            if (displayType.value == "DifferenceYX")
            {
                value = vector2.GetReference().GetDifferenceYX();
            }

            var min = Mathf.NegativeInfinity;
            var max = Mathf.Infinity;

            if (clampMinToZero)
            {
                min = 0;
            }

            if (clampMaxToY)
            {
                max = vector2.GetReference().Y;
            }

            value = Mathf.Clamp(value, min, max);

            if (roundUp)
                value = Mathf.Ceil(value);

            text = value.ToString(format);
        }
    }
}