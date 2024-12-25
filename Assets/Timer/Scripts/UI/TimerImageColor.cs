using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImageColor : MonoBehaviour
    {
        // public enum DisplayType
        // {
        //     Elapsed,
        //     TimeLeft
        // }

        // public DisplayType displayType;

        // public TimerSource timer;
        // public Image image;

        public TimerImage timerImage;

        public bool changeColorOnDurationReached;
        private Color originalColor;
        public Color colorOnDurationReached;

        void Start()
        {
            if (timerImage.displayType == DisplayType.Elapsed)
            {
                originalColor = timerImage.image.color;
            }

            if (timerImage.displayType == DisplayType.TimeLeft)
            {
                originalColor = timerImage.image.color;
            }
        }

        void Update()
        {
            if (changeColorOnDurationReached && timerImage.timer.GetReference().IsDurationReached())
            {
                if (timerImage.displayType == DisplayType.Elapsed)
                {
                    timerImage.image.color = colorOnDurationReached;
                }

                if (timerImage.displayType == DisplayType.TimeLeft)
                {
                    timerImage.image.color = colorOnDurationReached;
                }
            }
            else
            {
                if (timerImage.displayType == DisplayType.Elapsed)
                {
                    timerImage.image.color = originalColor;
                }

                if (timerImage.displayType == DisplayType.TimeLeft)
                {
                    timerImage.image.color = originalColor;
                }
            }
        }
    }
}