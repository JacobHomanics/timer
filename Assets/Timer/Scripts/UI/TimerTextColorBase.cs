using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{

    public class TimerTextColorBase : TimerImageColorBase
    {
        public Text text;

        protected override Color GetOriginalColor()
        {
            return text.color;
        }

        void Update()
        {
            SetColor(out Color color, GetReference(), displayType.value);
            text.color = color;
        }
    }

}
