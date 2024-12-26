namespace JacobHomanics.Core.Timer
{
    public class Vector2Reference : Vector2Source
    {
        public Vector2 timer;

        public override Vector2 GetReference()
        {
            return timer;
        }
    }
}
