using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerSlider : MonoBehaviour
    {
        public enum DisplayType
        {
            Elapsed,
            TimeLeft
        }

        public DisplayType displayType;

        public TimerSource timer;
        public Slider slider;

        public bool changeColorOnDurationReached;
        public Color colorOnDurationReached;

        private Color originalColor;

        void Start()
        {
            if (displayType == DisplayType.Elapsed)
            {
                originalColor = slider.fillRect.GetComponent<Image>().color;
            }

            if (displayType == DisplayType.TimeLeft)
            {
                originalColor = slider.transform.Find("Background").GetComponent<Image>().color;
            }
        }

        void Update()
        {
            if (changeColorOnDurationReached && timer.GetReference().IsDurationReached())
            {
                if (displayType == DisplayType.Elapsed)
                {
                    slider.fillRect.GetComponent<Image>().color = colorOnDurationReached;
                }

                if (displayType == DisplayType.TimeLeft)
                {
                    slider.transform.Find("Background").GetComponent<Image>().color = colorOnDurationReached;
                }
            }
            else
            {
                if (displayType == DisplayType.Elapsed)
                {
                    slider.fillRect.GetComponent<Image>().color = originalColor;
                }

                if (displayType == DisplayType.TimeLeft)
                {
                    slider.transform.Find("Background").GetComponent<Image>().color = originalColor;
                }
            }

            if (displayType == DisplayType.Elapsed)
            {
                slider.value = timer.GetReference().data.elapsedTime;
                slider.maxValue = timer.GetReference().data.duration;
            }

            if (displayType == DisplayType.TimeLeft)
            {
                slider.value = timer.GetReference().GetTimeLeft();
                slider.maxValue = timer.GetReference().data.duration;
            }
        }
    }
}