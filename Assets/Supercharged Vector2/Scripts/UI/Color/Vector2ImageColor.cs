using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Core.SuperchargedVector2.UI
{
    public class Vector2ImageColor : Vector2ColorBase
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