using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImage : TimerSource
    {
        public override Timer GetReference()
        {
            return timer.GetReference();
        }

        [Header("Configuration")]
        public DisplayType displayType;

        [Header("References")]
        public TimerSource timer;
        public Image image;

        void Update()
        {
            if (displayType.value == "ElapsedTime")
            {
                image.fillAmount = timer.GetReference().data.elapsedTime / timer.GetReference().data.duration;
            }

            if (displayType.value == "TimeLeft")
            {
                image.fillAmount = timer.GetReference().GetTimeLeft() / timer.GetReference().data.duration;
            }
        }
    }
}