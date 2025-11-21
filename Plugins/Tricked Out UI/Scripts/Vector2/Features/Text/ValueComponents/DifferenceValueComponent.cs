using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public class DifferenceValueComponent : BaseValueComponent
    {
        public override float GetValue(Vector2 vector)
        {
            return vector.y - vector.x;
        }
    }
}

