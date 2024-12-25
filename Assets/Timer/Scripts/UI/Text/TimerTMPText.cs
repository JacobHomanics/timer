using TMPro;

namespace JacobHomanics.Core.Timer.UI
{
    public class TimerTMPText : TimerTextBase
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