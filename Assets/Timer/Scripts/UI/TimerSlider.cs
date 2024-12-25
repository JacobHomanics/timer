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

        public DisplayType displayType;

        public TimerSource timer;
        public Slider slider;

        void Update()
        {
            if (displayType.value == DisplayType.Options.Elapsed)
            {
                slider.value = timer.GetReference().data.elapsedTime;
                slider.maxValue = timer.GetReference().data.duration;
            }

            if (displayType.value == DisplayType.Options.TimeLeft)
            {
                slider.value = timer.GetReference().GetTimeLeft();
                slider.maxValue = timer.GetReference().data.duration;
            }
        }
    }
}