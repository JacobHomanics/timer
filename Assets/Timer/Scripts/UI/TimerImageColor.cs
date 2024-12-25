using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImageColor : TimerImageColorBase
    {
        public Image image;

        protected override Color GetOriginalColor()
        {
            return image.color;
        }

        void Update()
        {
            SetColor(image, GetReference(), displayType.value);
        }
    }
}