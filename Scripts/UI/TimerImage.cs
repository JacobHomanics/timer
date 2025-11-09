using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.TimerSystem
{
    public class TimerImage : MonoBehaviour
    {
        [SerializeField] private Timer timer;

        public Timer Timer
        {
            get => timer;
        }

        [SerializeField] private Image image;

        public Image Image
        {
            get => image;
        }

        [SerializeReference]
        private List<FeatureToggle> featureToggles = new List<FeatureToggle>
    {
        new TextDisplayFeature2(),
        new ColorGradientFeature(),
        new BackgroundFillFeature(),
        new FlashingFeature()
    };

        public float CurrentNum => Timer.ElapsedTime;
        public float MaxNum => Timer.Duration;

        public bool reverseFill;

        private float previousValue;

        private void Awake()
        {
            previousValue = CurrentNum;
        }

        void Update()
        {
            Debug.Log(reverseFill);
            if (reverseFill)
                image.fillAmount = (MaxNum - CurrentNum) / MaxNum;
            else
                image.fillAmount = CurrentNum / MaxNum;



            UIToolkit.Display(UIToolkit.GetFeature<TextDisplayFeature2>(featureToggles), CurrentNum, MaxNum);

            UIToolkit.ColorGradientFeatureCommand(UIToolkit.GetFeature<ColorGradientFeature>(featureToggles), image, CurrentNum, MaxNum);

            UIToolkit.FlashingFeatureCommand(UIToolkit.GetFeature<FlashingFeature>(featureToggles), CurrentNum, MaxNum);

            UIToolkit.HandleValueChange(CurrentNum, UIToolkit.GetFeature<BackgroundFillFeature>(featureToggles), ref previousValue, MaxNum);
            UIToolkit.UpdateBackgroundFillAnimation(UIToolkit.GetFeature<BackgroundFillFeature>(featureToggles), MaxNum);
        }

    }
}
