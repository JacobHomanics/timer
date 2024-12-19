using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImage : MonoBehaviour
    {
        public enum Configuration
        {
            Elapsed,
            TimeLeft
        }

        public Configuration configuration;

        public Timer timer;
        public Image image;

        void Update()
        {
            if (configuration == Configuration.Elapsed)
            {
                image.fillAmount = timer.elapsedTime / timer.duration;
            }

            if (configuration == Configuration.TimeLeft)
            {
                image.fillAmount = timer.GetTimeLeft() / timer.duration;
            }
        }
    }
}