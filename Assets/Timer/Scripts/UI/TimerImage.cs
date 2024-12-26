using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImage : Vector2Source
    {
        public override Vector2 GetReference()
        {
            return timer.GetReference();
        }

        [Header("Configuration")]
        public DisplayType displayType;

        [Header("References")]
        public Vector2Source timer;
        public Image image;

        void Update()
        {
            if (displayType.value == "ElapsedTime")
            {
                image.fillAmount = timer.GetReference().X / timer.GetReference().Y;
            }

            if (displayType.value == "TimeLeft")
            {
                image.fillAmount = timer.GetReference().GetDifferenceYX() / timer.GetReference().Y;
            }
        }
    }
}