using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that handles animated background fill based on value changes.
    /// </summary>
    public class BackgroundFillImage : BackgroundFillFeatureComponent
    {
        public Image image;

        void Update()
        {
            image.fillAmount = HandleValueChange(Current, image.fillAmount, backgroundFillFeature.keepSizeConsistent, ref previousValue, Max, backgroundFillFeature.delay, backgroundFillFeature.speedMultiplierCurve, backgroundFillFeature.animationSpeed);
            image.fillAmount = UpdateBackgroundFillAnimation(image.fillAmount, Max);
        }

        public void Reset()
        {
            if (!image)
                image = GetComponent<Image>();
        }
    }
}

