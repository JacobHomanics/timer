using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImageColor : TimerImageColorBase
    {
        public Image image;
        public TimerSource timer;

        public DisplayType displayType;

        public override Timer GetReference()
        {
            return timer.GetReference();
        }

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