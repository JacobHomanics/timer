using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public class EncloseInBracesComponent : BaseTextFeatureComponent
    {
        [SerializeField] private bool _showPercentageSymbol = true;

        public bool showPercentageSymbol
        {
            get => _showPercentageSymbol;
            set => _showPercentageSymbol = value;
        }

        public override float ProcessValue(float value, float max, ref float minValue, ref float maxValue)
        {
            // This component doesn't modify the value, only affects text formatting
            return value;
        }
    }
}

