using UnityEngine;
using JacobHomanics.TimerSystem;
using JacobHomanics.UI;

namespace JacobHomanics.TimerSystem.UI
{
    public class TimerConnector : BaseCurrentMaxConnector
    {
        [SerializeField] private Timer timer;

        public Timer Timer
        {
            get => timer;
        }

        public override float CurrentNum => Timer.ElapsedTime;
        public override float MaxNum => Timer.Duration;
    }
}