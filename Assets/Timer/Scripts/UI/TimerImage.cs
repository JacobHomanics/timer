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

        public bool changeColorOnDurationReached;
        private Color originalColor;
        public Color colorOnDurationReached;

        void Start()
        {
            if (displayType == DisplayType.Elapsed)
            {
                originalColor = image.color;
            }

            if (displayType == DisplayType.TimeLeft)
            {
                originalColor = image.color;
            }
        }

        void Update()
        {
            if (changeColorOnDurationReached && timer.GetReference().IsDurationReached())
            {
                if (displayType == DisplayType.Elapsed)
                {
                    image.color = colorOnDurationReached;
                }

                if (displayType == DisplayType.TimeLeft)
                {
                    image.color = colorOnDurationReached;
                }
            }
            else
            {
                if (displayType == DisplayType.Elapsed)
                {
                    image.color = originalColor;
                }

                if (displayType == DisplayType.TimeLeft)
                {
                    image.color = originalColor;
                }
            }
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