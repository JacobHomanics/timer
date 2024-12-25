using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerUI : MonoBehaviour
    {
        public Timer timer;

        public Text text;

        public enum DisplayType { Duration, ElapsedTime, TimeLeft }

        public DisplayType displayType;

        public void Update()
        {
            if (displayType == DisplayType.Duration)
            {
                text.text = timer.duration.ToString();
            }

            if (displayType == DisplayType.ElapsedTime)
            {
                text.text = timer.elapsedTime.ToString();
            }

            if (displayType == DisplayType.TimeLeft)
            {
                text.text = timer.GetTimeLeft().ToString();
            }
        }
    }

}

