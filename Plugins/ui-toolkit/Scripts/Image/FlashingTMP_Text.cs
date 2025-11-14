using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public class FlashingTMP_Text : FlashingFeatureComponent
    {
        public TMP_Text text;

        void Update()
        {
            // text.enabled = IsEnabled(thresholdType, Current, Max, thresholdPercent);
            text.color = CalcColor(flashSpeed, flashColor1, flashColor2);
        }
    }
}

