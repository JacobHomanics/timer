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

        [Header("Configuration")]
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
            return vector2.GetDifferenceYX();
        }

        public bool IsDurationReached()
        {
            return vector2.IsXGreatherThanOrEqualToY();
        }

        public void SetElapsedTime(float value)
        {
            vector2.SetX(value);
        }

        [ContextMenu("Set Elapsed To Zero")]
        public void SetElapsedToZero()
        {
            vector2.SetXToZero();
        }
    }
}