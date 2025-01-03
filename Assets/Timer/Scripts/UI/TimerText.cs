using UnityEngine.UI;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerText : TimerTextBase
    {
        public Text text;

        public void Update()
        {
            var theString = text.text;
            SetText(ref theString, displayType, format, clampTextToBounds, minTextBounds);
            text.text = theString;
        }
    }
}