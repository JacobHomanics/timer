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
                image.fillAmount = timer.GetReference().ElapsedTime / timer.GetReference().Duration;
            }

            if (displayType.value == "TimeLeft")
            {
                image.fillAmount = timer.GetReference().GetTimeLeft() / timer.GetReference().Duration;
            }
        }
    }
}