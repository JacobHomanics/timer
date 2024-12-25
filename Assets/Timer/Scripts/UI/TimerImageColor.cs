using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImageColor : TimerImageColorBase
    {
        public TimerImage timerImage;

        public override Timer GetReference()
        {
            return timerImage.timer.GetReference();
        }

        protected override Color GetOriginalColor()
        {
            return timerImage.image.color;
        }

        void Update()
        {
            SetColor(timerImage.image, GetReference(), timerImage.displayType);
        }
    }
}