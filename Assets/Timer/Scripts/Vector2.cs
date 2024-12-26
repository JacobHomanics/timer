using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.Core.Timer
{
    public class Vector2 : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Vector2 _value = new();

        public float X
        {
            get => _value.x;
            set
            {
                var previousValue = _value.x;
                _value.x = value;

                if (_value.x == 0)
                {
                    OnXSetToZero?.Invoke();
                }

                if (_value.x <= 0)
                {
                    OnXSetLessThanOrEqualToZero?.Invoke();
                }

                if (_value.x >= Y)
                {
                    OnXSetGreaterThanOrEqualToY?.Invoke();
                }

                if (_value.x == Y)
                {
                    OnXSetToY?.Invoke();
                }

                if (_value.x > previousValue)
                {
                    OnXIncrease?.Invoke();
                }

                if (_value.x < previousValue)
                {
                    OnXDecrease?.Invoke();
                }

                OnXSet?.Invoke();
            }
        }

        public float Y
        {
            get => _value.y;
            set
            {
                var previousValue = _value.y;

                _value.y = value;

                if (_value.y == 0)
                {
                    OnYSetToZero?.Invoke();
                }

                if (_value.y <= 0)
                {
                    OnXSetLessThanOrEqualToZero?.Invoke();
                }

                if (_value.y >= X)
                {
                    OnYSetGreaterThanOrEqualToX?.Invoke();
                }

                if (_value.y == X)
                {
                    OnYSetToX?.Invoke();
                }

                if (_value.y > previousValue)
                {
                    OnYIncrease?.Invoke();
                }

                if (_value.y < previousValue)
                {
                    OnYDecrease?.Invoke();
                }

                OnYSet?.Invoke();
            }
        }

        [Header("Events")]
        public UnityEvent OnXSet = new();
        public UnityEvent OnXSetLessThanOrEqualToZero = new();
        public UnityEvent OnXSetToZero = new();
        public UnityEvent OnXSetGreaterThanOrEqualToY = new();

        public UnityEvent OnXSetToY = new();
        public UnityEvent OnXChangedByOffset = new();
        public UnityEvent OnXIncrease;
        public UnityEvent OnXDecrease;

        public UnityEvent OnYSet = new();
        public UnityEvent OnYSetToZero = new();
        public UnityEvent OnYSetLessThanOrEqualToZero = new();
        public UnityEvent OnYSetGreaterThanOrEqualToX = new();
        public UnityEvent OnYSetToX = new();
        public UnityEvent OnYChangedByOffset = new();
        public UnityEvent OnYIncrease;
        public UnityEvent OnYDecrease;

        public void OffsetX(float value)
        {
            X += value;
            OnXChangedByOffset?.Invoke();
        }
        public void OffsetY(float value)
        {
            X -= value;
            OnYChangedByOffset?.Invoke();
        }

        public float GetDifferenceXY()
        {
            return X - Y;
        }

        public float GetDifferenceYX()
        {
            return Y - X;
        }

        public bool IsXEqualToY()
        {
            return X == Y;
        }

        public bool IsYEqualToX()
        {
            return Y == X;
        }

        public bool IsXGreatherThanOrEqualToY()
        {
            return X >= Y;
        }

        public bool IsXZero()
        {
            return X == 0;
        }

        public bool IsYZero()
        {
            return Y == 0;
        }

        public bool IsXZeroOrLess()
        {
            return X <= 0;
        }

        public bool IsYZeroOrLess()
        {
            return Y <= 0;
        }

        public bool IsXLessThanZero()
        {
            return X < 0;
        }

        public bool IsYLessThanZero()
        {
            return Y < 0;
        }

        [ContextMenu("-- X --")]
        private void AddX() { }
        [ContextMenu("Add 1")]
        private void AddX1()
        {
            OffsetX(1);
        }

        [ContextMenu("Add 10")]
        private void AddX10()
        {
            OffsetX(10);
        }

        [ContextMenu("Add 100")]
        private void AddX100()
        {
            OffsetX(100);
        }


        [ContextMenu("Add 1000")]
        private void AddX1000()
        {
            OffsetX(1000);
        }


        [ContextMenu("Subtract 1")]
        private void SubtractX1()
        {
            OffsetX(-1);
        }

        [ContextMenu("Subtract 10")]
        private void SubtractX10()
        {
            OffsetX(-10);
        }

        [ContextMenu("Subtract 100")]
        private void SubtractX100()
        {
            OffsetX(-100);
        }


        [ContextMenu("Subtract 1000")]
        private void SubtractX1000()
        {
            OffsetX(-1000);
        }


    }
}