using UnityEngine;
using TMPro;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerTMPTextColor : TimerImageColorBase
    {
        public TMP_Text text;

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
