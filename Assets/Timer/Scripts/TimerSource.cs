using UnityEngine;

namespace JacobHomanics.Core.Timer
{
    public abstract class TimerSource : MonoBehaviour
    {
        public abstract Timer GetReference();
    }
}
