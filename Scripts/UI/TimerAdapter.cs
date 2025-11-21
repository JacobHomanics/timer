using UnityEngine;
using JacobHomanics.TrickedOutUI;

namespace JacobHomanics.Timer.Tricked
{
    public class TimerAdapter : BaseVector2Adapter
    {
        [SerializeField] private Timer timer;

        public bool reverseFill = true;

        public Timer Timer
        {
            get => timer;
        }

        public override float X { get { if (reverseFill) return Timer.Duration - Timer.ElapsedTime; return Timer.ElapsedTime; } }
        public override float Y => Timer.Duration;
    }
}