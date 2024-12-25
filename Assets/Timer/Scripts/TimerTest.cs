using UnityEngine;

namespace JacobHomanics.Core.Timer
{
    public class TimerTest : MonoBehaviour
    {
        public TimerSource timer;

        void Start()
        {
            timer.GetReference().data.duration = 10f;

        }
    }
}
