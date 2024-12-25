using UnityEngine;

namespace JacobHomanics.Core.Timer
{
    public class TimerReference : TimerBase
    {
        public Timer timer;

        public override Timer GetReference()
        {
            return timer;
        }
    }
}
