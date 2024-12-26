using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.Core.Timer
{
    public class Timer : TimerSource
    {
        public override Timer GetReference()
        {
            return this;
        }

        public enum TickType
        {
            DeltaTime, UnscaledDeltaTime, SmoothDeltaTime, FixedDeltaTime, FixedUnscaledDeltaTime
        }

        [System.Serializable]
        public struct TimerData
        {
            public TimerData(TickType tickType, Vector2 value)
            {
                this.value = value;
                this.tickType = tickType;
                // this.b = duration;
                // this.a = elapsedTime;
            }

            public TickType tickType;

            // public float a;
            // public float b;

            public Vector2 value;
        }

        [Header("Configuration")]
        public TimerData data = new(TickType.DeltaTime, new Vector2(0f, 5f));

        public float ElapsedTime
        {
            get => data.value.x;
            set => data.value.x = value;
        }

        public float Duration
        {
            get => data.value.y;
            set => data.value.y = value;
        }

        [Header("Events")]
        public UnityEvent OnTick;
        public UnityEvent OnDurationElapsed;

        ////////////////////////////
        // Core
        ////////////////////////////

        public void Tick(float delta)
        {
            ElapsedTime += delta;
            OnTick?.Invoke();
        }

        ////////////////////////////
        //Monobehaviour
        ////////////////////////////

        void Update()
        {
            if (data.tickType == TickType.DeltaTime)
            {
                Tick(Time.deltaTime);
            }

            if (data.tickType == TickType.SmoothDeltaTime)
            {
                Tick(Time.smoothDeltaTime);
            }

            if (data.tickType == TickType.UnscaledDeltaTime)
            {
                Tick(Time.unscaledDeltaTime);
            }


            if (IsDurationReached())
            {
                OnDurationElapsed?.Invoke();
            }
        }

        void FixedUpdate()
        {
            if (data.tickType == TickType.FixedDeltaTime)
            {
                Tick(Time.fixedDeltaTime);
            }

            if (data.tickType == TickType.FixedUnscaledDeltaTime)
            {
                Tick(Time.fixedUnscaledDeltaTime);
            }
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

        public void SetElapsedTime(float value)
        {
            ElapsedTime = value;
        }

        [ContextMenu("Set Elapsed To Zero")]
        public void SetElapsedToZero()
        {
            ElapsedTime = 0;
        }
    }
}