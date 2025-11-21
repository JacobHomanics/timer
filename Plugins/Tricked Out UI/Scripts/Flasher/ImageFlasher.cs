using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public class ImageFlasher : BaseFlasher
    {
        public Image image;

        void Update()
        {
            image.color = CalcColor(flashDuration, flashColor1, flashColor2);
        }

        public void Reset()
        {
            if (!image)
                image = GetComponent<Image>();
        }
    }
}

