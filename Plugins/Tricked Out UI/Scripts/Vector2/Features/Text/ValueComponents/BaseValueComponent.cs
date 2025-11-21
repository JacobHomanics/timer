using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public abstract class BaseValueComponent : MonoBehaviour
    {
        public abstract float GetValue(Vector2 vector);
    }
}

