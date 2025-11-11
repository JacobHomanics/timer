using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Timer
{
    public class TextColor : ColorBase
    {
        public TMP_Text text;

        protected override Color GetOriginalColor()
        {
            return text.color;
        }

        void Update()
        {
            SetColor(out Color color, timer);
            text.color = color;
        }
    }
}