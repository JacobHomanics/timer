using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public class DefaultCurrentMaxConnector : BaseCurrentMaxConnector
    {
        [SerializeField] private float currentNum;
        [SerializeField] private float maxNum;

        public override float CurrentNum => currentNum;
        public override float MaxNum => maxNum;
    }
}
