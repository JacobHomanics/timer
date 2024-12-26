using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.Core.Timer
{
    [RequireComponent(typeof(Vector2))]
    public class Timer : MonoBehaviour
    {
        void Reset()
        {
            vector2 = GetComponent<Vector2>();
        }

        public enum TickType
        {
            DeltaTime, UnscaledDeltaTime, SmoothDeltaTime, FixedDeltaTime, FixedUnscaledDeltaTime
        }

        public Vector2 vector2;

        [Header("Configuration")]
        public TickType tickType;

        public float ElapsedTime
        {
            get => vector2.X;
            set => vector2.X = value;
        }

        public float Duration
        {
            get => vector2.Y;
            set => vector2.Y = value;
        }

        [Header("Events")]
        public UnityEvent OnTick = new();
        public UnityEvent OnDurationElapsed = new();
        public UnityEvent OnElapsedTimeReset = new();
        public UnityEvent OnElapsedTimeChangedByOffset = new();
        public UnityEvent OnDurationChangedByOffset = new();

        ////////////////////////////
        //Monobehaviour
        ////////////////////////////

        void OnEnable()
        {
            vector2.OnXSetGreaterThanOrEqualToY.AddListener(OnXGreaterThanOrEqualToY);
            vector2.OnXChangedByOffset.AddListener(OnXChangedByOffset);
            vector2.OnYChangedByOffset.AddListener(OnYChangedByOffset);
            vector2.OnXSetToZero.AddListener(OnXSetToZero);
        }

        void OnDisable()
        {
            vector2.OnXSetGreaterThanOrEqualToY.RemoveListener(OnXGreaterThanOrEqualToY);
            vector2.OnXChangedByOffset.RemoveListener(OnXChangedByOffset);
            vector2.OnYChangedByOffset.RemoveListener(OnYChangedByOffset);
            vector2.OnXSetToZero.RemoveListener(OnXSetToZero);
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
            OffsetElapsedTime(delta);
            OnTick?.Invoke();
        }

        private void OnXGreaterThanOrEqualToY()
        {
            OnDurationElapsed?.Invoke();
        }

        private void OnXChangedByOffset()
        {
            OnElapsedTimeChangedByOffset?.Invoke();
        }

        private void OnYChangedByOffset()
        {
            OnDurationChangedByOffset?.Invoke();
        }

        private void OnXSetToZero()
        {
            OnElapsedTimeReset?.Invoke();
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

        // public float GetTimeLeft()
        // {
        //     return vector2.GetDifferenceYX();
        // }

        // public bool IsDurationReached()
        // {
        //     return vector2.IsXGreatherThanOrEqualToY();
        // }

        public void OffsetElapsedTime(float value)
        {
            ElapsedTime += value;
        }

        public void ResetElapsedTime()
        {
            ElapsedTime = 0;
        }
    }
}