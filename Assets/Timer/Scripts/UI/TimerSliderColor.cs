using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSliderColor : MonoBehaviour
    {
        public TimerSlider timerSlider;

        public bool changeColorOnDurationReached;
        public Color colorOnDurationReached;

        private Color originalColor;

        void Start()
        {
            if (timerSlider.displayType == DisplayType.Elapsed)
            {
                originalColor = timerSlider.slider.fillRect.GetComponent<Image>().color;
            }

            if (timerSlider.displayType == DisplayType.TimeLeft)
            {
                originalColor = timerSlider.slider.transform.Find("Background").GetComponent<Image>().color;
            }
        }

        void Update()
        {
            if (timerSlider.displayType == DisplayType.Elapsed)
            {
                SetColor(timerSlider.slider.fillRect.GetComponent<Image>(), timerSlider.timer, timerSlider.displayType);
            }

            if (timerSlider.displayType == DisplayType.TimeLeft)
            {
                SetColor(timerSlider.slider.transform.Find("Background").GetComponent<Image>(), timerSlider.timer, timerSlider.displayType);
            }
        }


        private void SetColor(Image image, TimerSource timer, DisplayType displayType)
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
        }
    }
}