using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.TimerSystem
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float duration;
        public float Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        [SerializeField] private float elapsedTime;
        public float ElapsedTime
        {
            get
            {
                return elapsedTime;
            }
            set
            {
                elapsedTime = value;
                if (elapsedTime >= Duration)
                    OnDurationReached?.Invoke();
            }
        }

        public enum TickType
        {
            DeltaTime, UnscaledDeltaTime, SmoothDeltaTime, FixedDeltaTime, FixedUnscaledDeltaTime
        }

        [Header("Configuration")]
        public TickType tickType;

        [Header("Events")]
        public UnityEvent OnTick = new();
        public UnityEvent OnDurationReached = new();
        // public UnityEvent OnElapsedTimeReset = new();
        // public UnityEvent OnElapsedTimeChangedByOffset = new();
        // public UnityEvent OnDurationChangedByOffset = new();

        ////////////////////////////
        //Monobehaviour
        ////////////////////////////

        void Update()
        {
            if (tickType == TickType.DeltaTime)
            {
                Tick(Time.deltaTime);
            }

            if (tickType == TickType.SmoothDeltaTime)
            {
                Tick(Time.smoothDeltaTime);
            }

            if (tickType == TickType.UnscaledDeltaTime)
            {
                Tick(Time.unscaledDeltaTime);
            }
        }

        void FixedUpdate()
        {
            if (tickType == TickType.FixedDeltaTime)
            {
                Tick(Time.fixedDeltaTime);
            }

            if (tickType == TickType.FixedUnscaledDeltaTime)
            {
                Tick(Time.fixedUnscaledDeltaTime);
            }
        }

        ////////////////////////////
        // Core
        ////////////////////////////

        public void Tick(float delta)
        {
            ElapsedTime += delta;
            OnTick?.Invoke();
        }

        private void OnXGreaterThanOrEqualToY()
        {
        }

        ////////////////////////////
        //Helper Methods
        ////////////////////////////

        public float GetTimeLeft()
        {
            return Duration - ElapsedTime;
        }

        public bool IsDurationReached()
        {
            return ElapsedTime >= Duration;
        }
    }
}