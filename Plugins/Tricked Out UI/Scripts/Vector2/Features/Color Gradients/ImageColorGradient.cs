using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that applies a color gradient to an image based on current and max values.
    /// Supports an arbitrary list of colors and thresholds.
    /// </summary>
    public class ImageColorGradient : BaseColorGradient
    {
        public Image image;

        void Update()
        {
            image.color = CalculateColor();
        }

        public void Reset()
        {
            if (!image)
                image = GetComponent<Image>();
        }
    }
}

