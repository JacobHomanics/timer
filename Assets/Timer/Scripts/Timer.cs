using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.Core.Timer
{
    [RequireComponent(typeof(Vector2))]
    public class Timer : TimerSource
    {
        public override Timer GetReference()
        {
            return this;
        }

        void Reset()
        {
            vector2 = GetComponent<Vector2>();
        }

        public enum TickType
        {
            DeltaTime, UnscaledDeltaTime, SmoothDeltaTime, FixedDeltaTime, FixedUnscaledDeltaTime
        }

        // [System.Serializable]
        // public struct TimerData
        // {
        //     public TimerData(TickType tickType, UnityEngine.Vector2 value)
        //     {
        //         this.value = value;
        //         this.tickType = tickType;
        //         // this.b = duration;
        //         // this.a = elapsedTime;
        //     }

        //     public TickType tickType;

        //     // public float a;
        //     // public float b;

        //     public UnityEngine.Vector2 value;
        // }

        [Header("Configuration")]
        // public TimerData data = new(TickType.DeltaTime, new UnityEngine.Vector2(0f, 5f));

        public TickType tickType;

        public Vector2 vector2;

        public float ElapsedTime
        {
            get => vector2.value.x;
            set => vector2.value.x = value;
        }

        public float Duration
        {
            get => vector2.value.y;
            set => vector2.value.y = value;
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


            if (IsDurationReached())
            {
                OnDurationElapsed?.Invoke();
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