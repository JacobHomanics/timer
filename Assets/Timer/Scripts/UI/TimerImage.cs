using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImage : MonoBehaviour
    {
        public enum DisplayType
        {
            Elapsed,
            TimeLeft
        }

        public DisplayType displayType;

        public TimerBase timerBase;

        public Timer timer;
        public Image image;

        void Update()
        {
            if (displayType == DisplayType.Elapsed)
            {
                Debug.Log(timerBase.GetReference().elapsedTime);

                image.fillAmount = timer.elapsedTime / timer.duration;
            }

            if (displayType == DisplayType.TimeLeft)
            {
                image.fillAmount = timer.GetTimeLeft() / timer.duration;
            }
        }
    }
}