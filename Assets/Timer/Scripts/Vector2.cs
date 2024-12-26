using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.Core.Timer
{
    public class Vector2 : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Vector2 _value;
        public float X
        {
            get => _value.x;
            set
            {
                _value.x = value;

                if (X <= 0)
                {
                    OnXSetLessThanOrEqualToZero?.Invoke();
                }

                if (X >= Y)
                {
                    OnXSetGreaterThanOrEqualToY?.Invoke();
                }
            }
        }

        public float Y
        {
            get => _value.y;
            set => _value.y = value;
        }

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

        public UnityEvent OnXSetLessThanOrEqualToZero;
        public UnityEvent OnXSetGreaterThanOrEqualToY;

        public void OffsetX(float value)
        {
            X += value;
        }
        public void OffsetY(float value)
        {
            X -= value;
        }
    }
}