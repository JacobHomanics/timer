using UnityEngine;
using TMPro;

namespace JacobHomanics.Core.Timer.UI
{
    public class Vector2TMPTextColor : TimerColorBase
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
