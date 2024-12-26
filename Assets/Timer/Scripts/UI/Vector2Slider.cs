using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class Vector2Slider : Vector2Source
    {
        public override Vector2 GetReference()
        {
            return vector2.GetReference();
        }

        [Header("Configuration")]
        public DisplayType displayType;

        [Header("References")]

        public Vector2Source vector2;
        public Slider slider;

        void Update()
        {
            if (displayType.value == "X")
            {
                slider.value = vector2.GetReference().X;
                slider.maxValue = vector2.GetReference().Y;
            }

            if (displayType.value == "DifferenceYX")
            {
                slider.value = vector2.GetReference().GetDifferenceYX();
                slider.maxValue = vector2.GetReference().Y;
            }
        }
    }
}