using TMPro;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that applies a color gradient to an image based on current and max values.
    /// Supports an arbitrary list of colors and thresholds.
    /// </summary>
    public class TMP_TextColorGradient : BaseColorGradient
    {
        public TMP_Text text;

        void Update()
        {
            text.color = CalculateColor();
        }

        void Reset()
        {
            if (!text)
                text = GetComponent<TMP_Text>();
        }
    }
}

