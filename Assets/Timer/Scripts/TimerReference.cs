using UnityEngine;

namespace JacobHomanics.Core.Timer
{
    public class TimerReference : MonoBehaviour, ITimer
    {
        public Timer timer;

        public Timer TimerInstance => timer;
    }
}
