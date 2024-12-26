using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class Vector2Image : Vector2Source
    {
        public override Vector2 GetReference()
        {
            return vector2.GetReference();
        }

        [Header("Configuration")]
        public DisplayType displayType;

        [Header("References")]
        public Vector2Source vector2;
        public Image image;

        void Update()
        {
            if (displayType.value == "X")
            {
                image.fillAmount = vector2.GetReference().X / vector2.GetReference().Y;
            }

            if (displayType.value == "DifferenceYX")
            {
                image.fillAmount = vector2.GetReference().GetDifferenceYX() / vector2.GetReference().Y;
            }
        }
    }
}