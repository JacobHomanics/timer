using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSlider : TimerSource
    {
        public override Timer GetReference()
        {
            return timer.GetReference();
        }

        [Header("Configuration")]
        public DisplayType displayType;

        [Header("References")]

        public TimerSource timer;
        public Slider slider;

        void Update()
        {
            if (displayType.value == "ElapsedTime")
            {
                slider.value = timer.GetReference().data.elapsedTime;
                slider.maxValue = timer.GetReference().data.duration;
            }

            if (displayType.value == "TimeLeft")
            {
                slider.value = timer.GetReference().GetTimeLeft();
                slider.maxValue = timer.GetReference().data.duration;
            }
        }
    }
}