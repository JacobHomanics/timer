using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImageColor : MonoBehaviour
    {
        public TimerImage timerImage;

        public bool changeColorOnDurationReached;
        private Color originalColor;
        public Color colorOnDurationReached;

        void Start()
        {
            originalColor = timerImage.image.color;
        }

        void Update()
        {
            SetColor(timerImage.image, timerImage.timer, timerImage.displayType);
        }

        private void SetColor(Image image, TimerSource timer, DisplayType displayType)
        {
            if (changeColorOnDurationReached && timerImage.timer.GetReference().IsDurationReached())
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
        }
    }
}