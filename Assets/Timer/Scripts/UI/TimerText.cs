using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerUI : MonoBehaviour
    {
        public Timer timer;

        public Text text;

        public string format = "F3";

        public enum DisplayType { Duration, ElapsedTime, TimeLeft }

        public DisplayType displayType;

        public void Update()
        {
            if (displayType == DisplayType.Duration)
            {
                text.text = timer.duration.ToString(format);
            }

            if (displayType == DisplayType.ElapsedTime)
            {
                text.text = timer.elapsedTime.ToString(format);
            }

            if (displayType == DisplayType.TimeLeft)
            {
                text.text = timer.GetTimeLeft().ToString(format);
            }
        }
    }

}

