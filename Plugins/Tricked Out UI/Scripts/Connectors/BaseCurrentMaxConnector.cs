using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public abstract class BaseCurrentMaxConnector : MonoBehaviour
    {
        public abstract float CurrentNum { get; }
        public abstract float MaxNum { get; }
    }
}
