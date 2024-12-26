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
            get => vector2.X;
            set => vector2.X = value;
        }

        public float Duration
        {
            get => vector2.Y;
            set => vector2.Y = value;
        }

        [Header("Events")]
        public UnityEvent OnTick;

        public UnityEvent OnDurationElapsed;

        void OnEnable()
        {
            vector2.OnXSetGreaterThanOrEqualToY.AddListener(() => { OnDurationElapsed?.Invoke(); });
        }

        void OnDisable()
        {
            vector2.OnXSetGreaterThanOrEqualToY.RemoveListener(() => { OnDurationElapsed?.Invoke(); });
        }

        ////////////////////////////
        // Core
        ////////////////////////////

        public void Tick(float delta)
        {
            AddElapsedTime(delta);
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

        public void AddElapsedTime(float value)
        {
            ElapsedTime += value;
        }

        public void SetElapsedTime(float value)
        {
            ElapsedTime = value;
        }

        public void ResetElapsedTime()
        {
            ElapsedTime = 0;
        }
    }
}