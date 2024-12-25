using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSlider : MonoBehaviour
    {
        public enum DisplayType
        {
            Elapsed,
            TimeLeft
        }

        public DisplayType displayType;

        public TimerBase timer;
        public Slider slider;

        void Update()
        {
            if (displayType == DisplayType.Elapsed)
            {
                slider.value = timer.GetReference().data.elapsedTime;
                slider.maxValue = timer.GetReference().data.duration;
            }

            if (displayType == DisplayType.TimeLeft)
            {
                slider.value = timer.GetReference().GetTimeLeft();
                slider.maxValue = timer.GetReference().data.duration;
            }
        }
    }
}