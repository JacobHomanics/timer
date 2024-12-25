using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImageWithText : MonoBehaviour
    {
        public enum DisplayType
        {
            Elapsed,
            TimeLeft
        }

        public DisplayType displayType;

        public Timer timer;
        public Image image;

        void Update()
        {
            if (displayType == DisplayType.Elapsed)
            {
                image.fillAmount = timer.elapsedTime / timer.duration;
            }

            if (displayType == DisplayType.TimeLeft)
            {
                image.fillAmount = timer.GetTimeLeft() / timer.duration;
            }
        }
    }
}