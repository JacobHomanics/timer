using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.Core.Timer
{
    public class Timer : TimerBase
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
            public TimerData(TickType tickType, float duration, float elapsedTime)
            {
                this.tickType = tickType;
                this.duration = duration;
                this.elapsedTime = elapsedTime;
            }

            public TickType tickType;

            public float duration;
            public float elapsedTime;
        }

        [Header("Configuration")]
        public TimerData data = new(TickType.DeltaTime, 5.0f, 0f);

        [Header("Events")]
        public UnityEvent OnTick;
        public UnityEvent OnDurationElapsed;

        ////////////////////////////
        // Core
        ////////////////////////////

        public void Tick(float delta)
        {
            data.elapsedTime += delta;
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


            if (data.elapsedTime >= data.duration)
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
            return data.duration - data.elapsedTime;
        }

        public void SetElapsedTime(float value)
        {
            data.elapsedTime = value;
        }
    }
}