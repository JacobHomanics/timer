namespace JacobHomanics.TrickedOutUI
{
    public class DifferenceValueComponent : BaseValueComponent
    {
        public override float GetValue(float current, float max)
        {
            return max - current;
        }
    }
}

