using UnityEngine;

namespace JacobHomanics.Core.Timer
{
    public class TimerReference : TimerSource
    {
        public Timer timer;

        public override Timer GetReference()
        {
            return timer;
        }
    }
}
