using UnityEngine;
using TMPro;

namespace JacobHomanics.TimerSystem
{
    public class TimerText : MonoBehaviour
    {
        [Header("Configuration")]
        public string format = "#,##0.####################";
        public bool clampToZero;
        public bool clampToMax;

        public bool roundUp;

        public enum DisplayType
        {
            Duration, ElapsedTime, TimeLeft
        }

        public DisplayType displayType;

        public Timer timer;

        public TMP_Text text;

        public void Update()
        {
            var theString = text.text;
            SetText(ref theString, displayType, format);
            text.text = theString;
        }

        protected void SetText(ref string text, DisplayType displayType, string format)
        {
            var value = 0f;

            if (displayType.ToString() == "Duration")
            {
                value = timer.Duration;
            }

            if (displayType.ToString() == "ElapsedTime")
            {
                value = timer.ElapsedTime;
            }

            if (displayType.ToString() == "TimeLeft")
            {
                value = timer.Duration - timer.ElapsedTime;
            }

            var min = Mathf.NegativeInfinity;
            var max = Mathf.Infinity;

            if (clampToZero)
            {
                min = 0;
            }

            if (clampToMax)
            {
                max = timer.Duration;
            }

            value = Mathf.Clamp(value, min, max);

            if (roundUp)
                value = Mathf.Ceil(value);

            text = value.ToString(format);
        }
    }
}