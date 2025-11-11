using UnityEngine;
using JacobHomanics.TrickedOutUI;

namespace JacobHomanics.Timer.Tricked
{
    public class TimerConnector : BaseCurrentMaxConnector
    {
        [SerializeField] private Timer timer;

        public bool reverseFill;

        public Timer Timer
        {
            get => timer;
        }

        public override float CurrentNum { get { if (reverseFill) return Timer.Duration - Timer.ElapsedTime; return Timer.ElapsedTime; } }
        public override float MaxNum => Timer.Duration;
    }
}