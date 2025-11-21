using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public class XValueComponent : BaseValueComponent
    {
        public override float GetValue(Vector2 vector)
        {
            return vector.x;
        }
    }
}

