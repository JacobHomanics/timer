using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Timer
{
    public class SliderColor : ColorBase
    {
        public Slider slider;

        public bool changeFillRect = true;

        protected override Color GetOriginalColor()
        {
            if (changeFillRect)
            {
                return slider.fillRect.GetComponent<Image>().color;
            }
            else
            {
                return slider.transform.Find("Background").GetComponent<Image>().color;
            }
        }

        void Update()
        {
            if (changeFillRect)
            {
                SetColor(out Color color, timer);
                slider.fillRect.GetComponent<Image>().color = color;
            }
            else
            {
                SetColor(out Color color, timer);
                slider.transform.Find("Background").GetComponent<Image>().color = color;
            }
        }
    }
}