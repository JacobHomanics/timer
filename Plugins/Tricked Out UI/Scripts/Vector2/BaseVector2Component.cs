using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public abstract class BaseVector2Component : MonoBehaviour
    {
        public BaseVector2Adapter vector2Adapter;

        public float X => vector2Adapter.X;
        public float Y => vector2Adapter.Y;
    }
}
