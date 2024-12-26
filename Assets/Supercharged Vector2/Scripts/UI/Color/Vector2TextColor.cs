using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.SuperchargedVector2.UI
{
    public class Vector2TextColor : Vector2ColorBase
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
