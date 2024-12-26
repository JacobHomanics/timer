using UnityEngine.Serialization;

namespace JacobHomanics.Core.Timer
{
    public class Vector2Reference : Vector2Source
    {
        [FormerlySerializedAs("timer")]
        public Vector2 vector2;

        public override Vector2 GetReference()
        {
            return vector2;
        }
    }
}
