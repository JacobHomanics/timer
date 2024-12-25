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

        public TimerSource timer;
        public Image image;

        void Update()
        {
            if (displayType == DisplayType.Elapsed)
            {
                image.fillAmount = timer.GetReference().data.elapsedTime / timer.GetReference().data.duration;
            }

            if (displayType == DisplayType.TimeLeft)
            {
                image.fillAmount = timer.GetReference().GetTimeLeft() / timer.GetReference().data.duration;
            }
        }
    }
}