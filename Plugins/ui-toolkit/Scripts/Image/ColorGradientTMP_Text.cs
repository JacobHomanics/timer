using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that applies a color gradient to an image based on current and max values.
    /// Supports an arbitrary list of colors and thresholds.
    /// </summary>
    public class ColorGradientTMP_Text : ColorGradientFeatureComponent
    {
        public TMP_Text text;

        void Update()
        {
            text.color = HandleColor();
        }
    }
}

