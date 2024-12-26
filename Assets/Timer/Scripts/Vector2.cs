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
            }
        }

        public float Y
        {
            get => _value.y;
            set
            {
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
            }
        }

        [Header("Events")]
        public UnityEvent OnXSetLessThanOrEqualToZero = new();
        public UnityEvent OnXSetToZero = new();
        public UnityEvent OnXSetGreaterThanOrEqualToY = new();
        public UnityEvent OnXChangedByOffset = new();

        public UnityEvent OnYSetToZero = new();
        public UnityEvent OnYSetLessThanOrEqualToZero = new();
        public UnityEvent OnYSetGreaterThanOrEqualToX = new();
        public UnityEvent OnYChangedByOffset = new();


        public float GetDifferenceXY()
        {
            return X - Y;
        }

        public float GetDifferenceYX()
        {
            return Y - X;
        }

        public bool IsXGreatherThanOrEqualToY()
        {
            return X >= Y;
        }

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
    }
}