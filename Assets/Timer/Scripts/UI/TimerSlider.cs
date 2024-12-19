using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSlider : MonoBehaviour
    {
        public enum Configuration
        {
            Elapsed,
            TimeLeft
        }

        public Configuration configuration;

        public Timer timer;
        public Slider slider;

        void Update()
        {
            if (configuration == Configuration.Elapsed)
            {
                slider.value = timer.elapsedTime;
                slider.maxValue = timer.duration;
            }

            if (configuration == Configuration.TimeLeft)
            {
                slider.value = timer.GetTimeLeft();
                slider.maxValue = timer.duration;
            }
        }
    }
}