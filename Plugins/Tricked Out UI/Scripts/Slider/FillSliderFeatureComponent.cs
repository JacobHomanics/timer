using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    public class FillSliderFeatureComponent : BaseCurrentMaxComponent
    {
        public Slider slider;
        void Update()
        {
            slider.value = Current;
            slider.maxValue = Max;
        }
    }
}