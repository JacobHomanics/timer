using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    public class SliderVector2 : BaseVector2Component
    {
        public Slider slider;
        void Update()
        {
            slider.value = X;
            slider.maxValue = Y;
        }

        void Reset()
        {
            if (!slider)
                slider = GetComponent<Slider>();
        }
    }
}