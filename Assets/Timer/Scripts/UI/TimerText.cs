using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerUI : MonoBehaviour
    {
        public Timer timer;

        public Text text;

        public enum PropertyDisplayer { Duration, ElapsedTime, TimeLeft }

        public PropertyDisplayer propertyDisplayer;

        public void Update()
        {
            if (propertyDisplayer == PropertyDisplayer.Duration)
            {
                text.text = timer.duration.ToString();
            }

            if (propertyDisplayer == PropertyDisplayer.ElapsedTime)
            {
                text.text = timer.elapsedTime.ToString();
            }

            if (propertyDisplayer == PropertyDisplayer.TimeLeft)
            {
                text.text = timer.GetTimeLeft().ToString();
            }
        }
    }

}

