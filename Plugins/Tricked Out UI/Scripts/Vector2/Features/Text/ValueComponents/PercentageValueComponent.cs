using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public class PercentageValueComponent : BaseValueComponent
    {
        public override float GetValue(Vector2 vector)
        {
            return vector.x / vector.y * 100f;
        }
    }
}

