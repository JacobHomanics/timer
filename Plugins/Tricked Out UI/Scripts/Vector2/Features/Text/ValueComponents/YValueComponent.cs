using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public class YValueComponent : BaseValueComponent
    {
        public override float GetValue(Vector2 vector)
        {
            return vector.y;
        }
    }
}

