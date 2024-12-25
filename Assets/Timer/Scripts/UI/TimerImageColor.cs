using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerImageColor : TimerColorBase
    {
        public Image image;

        protected override Color GetOriginalColor()
        {
            return image.color;
        }

        void Update()
        {
            SetColor(out Color color, GetReference(), displayType.value);
            image.color = color;
        }
    }
}