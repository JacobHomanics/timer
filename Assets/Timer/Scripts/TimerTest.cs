using UnityEngine;

namespace JacobHomanics.Core.Timer
{
    public class TimerTest : MonoBehaviour
    {
        // [RequireInterface(typeof(ITimer))]
        // public MonoBehaviour timer2;
        // public InterfaceReference<ITimer> timer;

        public TimerBase timer;

        void Start()
        {
            timer.GetReference().duration = 10f;
        }
    }
}
