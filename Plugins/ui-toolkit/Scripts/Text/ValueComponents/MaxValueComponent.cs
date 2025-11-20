namespace JacobHomanics.TrickedOutUI
{
    public class MaxValueComponent : BaseValueComponent
    {
        public override float GetValue(float current, float max)
        {
            return max;
        }
    }
}

