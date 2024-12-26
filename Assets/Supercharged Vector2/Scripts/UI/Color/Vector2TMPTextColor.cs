using UnityEngine;
using TMPro;

namespace JacobHomanics.Core.SuperchargedVector2.UI
{
    public class Vector2TMPTextColor : Vector2ColorBase
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
