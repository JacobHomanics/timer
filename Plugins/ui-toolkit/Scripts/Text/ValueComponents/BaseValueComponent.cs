namespace JacobHomanics.TrickedOutUI
{
    public abstract class BaseValueComponent : BaseCurrentMaxComponent
    {
        public abstract float GetValue(float current, float max);
    }
}

