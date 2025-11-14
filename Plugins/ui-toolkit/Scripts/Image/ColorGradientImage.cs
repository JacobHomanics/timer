using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that applies a color gradient to an image based on current and max values.
    /// Supports an arbitrary list of colors and thresholds.
    /// </summary>
    public class ColorGradientImage : ColorGradientFeatureComponent
    {
        public Image image;

        void Update()
        {
            image.color = HandleColor();
        }
    }
}

