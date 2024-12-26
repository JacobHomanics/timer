using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSlider : Vector2Source
    {
        public override Vector2 GetReference()
        {
            return timer.GetReference();
        }

        [Header("Configuration")]
        public DisplayType displayType;

        [Header("References")]

        public Vector2Source timer;
        public Slider slider;

        void Update()
        {
            if (displayType.value == "ElapsedTime")
            {
                slider.value = timer.GetReference().X;
                slider.maxValue = timer.GetReference().Y;
            }

            if (displayType.value == "TimeLeft")
            {
                slider.value = timer.GetReference().GetDifferenceYX();
                slider.maxValue = timer.GetReference().Y;
            }
        }
    }
}