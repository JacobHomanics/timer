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
            if (changeColorOnDurationReached && timerSlider.timer.GetReference().IsDurationReached())
            {
                if (timerSlider.displayType == DisplayType.Elapsed)
                {
                    timerSlider.slider.fillRect.GetComponent<Image>().color = colorOnDurationReached;
                }

                if (timerSlider.displayType == DisplayType.TimeLeft)
                {
                    timerSlider.slider.transform.Find("Background").GetComponent<Image>().color = colorOnDurationReached;
                }
            }
            else
            {
                if (timerSlider.displayType == DisplayType.Elapsed)
                {
                    timerSlider.slider.fillRect.GetComponent<Image>().color = originalColor;
                }

                if (timerSlider.displayType == DisplayType.TimeLeft)
                {
                    timerSlider.slider.transform.Find("Background").GetComponent<Image>().color = originalColor;
                }
            }
        }
    }
}