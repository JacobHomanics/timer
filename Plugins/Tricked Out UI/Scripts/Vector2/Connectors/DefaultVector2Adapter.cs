using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    public class DefaultVector2Adapter : BaseVector2Adapter
    {
        [SerializeField] private float x;
        [SerializeField] private float y;

        public override float X => x;
        public override float Y => y;
    }
}
