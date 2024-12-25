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

        public Timer timer;
        public Slider slider;

        void Update()
        {
            if (displayType == DisplayType.Elapsed)
            {
                slider.value = timer.data.elapsedTime;
                slider.maxValue = timer.data.duration;
            }

            if (displayType == DisplayType.TimeLeft)
            {
                slider.value = timer.GetTimeLeft();
                slider.maxValue = timer.data.duration;
            }
        }
    }
}