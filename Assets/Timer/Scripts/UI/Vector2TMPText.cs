using TMPro;

namespace JacobHomanics.Core.Timer.UI
{
    public class Vector2TMPText : Vector2TextBase
    {
        public TMP_Text text;

        public void Update()
        {
            var theString = text.text;
            SetText(ref theString, displayType, format, clampTextToBounds, minTextBounds);
            text.text = theString;
        }
    }
}