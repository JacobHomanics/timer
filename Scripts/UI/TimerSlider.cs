using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JacobHomanics.TimerSystem
{
    public class TimerSlider : MonoBehaviour
    {
        [Header("References")]

        public Timer timer;
        public Slider slider;
        public bool reverseFill;

        void Update()
        {
            if (reverseFill)
            {
                slider.value = timer.Duration - timer.ElapsedTime;
                slider.maxValue = timer.Duration;
            }
            else
            {
                slider.value = timer.ElapsedTime;
                slider.maxValue = timer.Duration;
            }

        }
    }
}