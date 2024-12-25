using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.Core.Timer
{
    public class Timer : MonoBehaviour, ITimer
    {
        public Timer TimerInstance => this;

        public enum TickType
        {
            DeltaTime, UnscaledDeltaTime, SmoothDeltaTime, FixedDeltaTime, FixedUnscaledDeltaTime
        }

        [Header("Configuration")]
        public TickType tickType = TickType.DeltaTime;

        [Header("Properties")]
        public float duration = 5f;
        public float elapsedTime = 0f;

        [Header("Events")]
        public UnityEvent OnTick;
        public UnityEvent OnDurationElapsed;

        ////////////////////////////
        // Core
        ////////////////////////////

        public void Tick(float delta)
        {
            elapsedTime += delta;
            OnTick?.Invoke();
        }

        ////////////////////////////
        //Monobehaviour
        ////////////////////////////

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


            if (elapsedTime >= duration)
            {
                OnDurationElapsed?.Invoke();
            }
        }

        ////////////////////////////
        //Helper Methods
        ////////////////////////////
        public float GetTimeLeft()
        {
            return duration - elapsedTime;
        }

        public void SetElapsedTime(float value)
        {
            elapsedTime = value;
        }
    }
}