using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.TimerSystem
{
    public class ImageColor : ColorBase
    {
        public Image image;

        protected override Color GetOriginalColor()
        {
            return image.color;
        }

        void Update()
        {
            SetColor(out Color color, timer);
            image.color = color;
        }
    }
}