using UnityEngine;

namespace JacobHomanics.Core.Timer
{
    public class TimerTest : MonoBehaviour
    {
        public TimerBase timer;

        void Start()
        {
            timer.GetReference().data.duration = 10f;

        }
    }
}
