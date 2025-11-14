using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    public class FillImageFeatureComponent : BaseCurrentMaxComponent
    {
        [SerializeField] private Image image;

        public Image Image
        {
            get => image;
        }

        void Update()
        {
            image.fillAmount = Normalize(Current, Max);
        }

        private float Normalize(float current, float max)
        {
            return current / max;
        }
    }

}
