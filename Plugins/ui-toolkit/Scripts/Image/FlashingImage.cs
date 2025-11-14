using System;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public class FlashingImage : FlashingFeatureComponent
    {
        public Image flashImage;

        void Update()
        {
            flashImage.color = CalcColor(flashSpeed, flashColor1, flashColor2);
        }
    }
}

