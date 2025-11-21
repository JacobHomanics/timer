using TMPro;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public class TMP_TextFlasher : BaseFlasher
    {
        public TMP_Text text;

        void Update()
        {
            text.color = CalcColor(flashDuration, flashColor1, flashColor2);
        }

        void Reset()
        {
            if (!text)
                text = GetComponent<TMP_Text>();
        }
    }
}

