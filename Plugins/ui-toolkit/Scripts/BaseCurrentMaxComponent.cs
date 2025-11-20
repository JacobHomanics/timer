using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public abstract class BaseCurrentMaxComponent : MonoBehaviour
    {
        public float Current => GetComponentInParent<BaseCurrentMaxConnector>().CurrentNum;
        public float Max => GetComponentInParent<BaseCurrentMaxConnector>().MaxNum;
    }
}
