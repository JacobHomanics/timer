using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.Core.Timer
{
    public class Vector2 : MonoBehaviour
    {
        public UnityEngine.Vector2 value;

        public float GetDifferenceXY()
        {
            return value.x - value.y;
        }

        public float GetDifferenceYX()
        {
            return value.y - value.x;
        }

        public bool IsXGreatherThanOrEqualToY()
        {
            return value.x >= value.y;
        }

        [ContextMenu("Set X to Zero")]
        public void SetXToZero()
        {
            SetX(0);
        }

        public UnityEvent OnXSetLessThanOrEqualToZero;
        public UnityEvent OnXSetGreaterThanOrEqualToY;

        public void AddX(float value)
        {
            SetX(this.value.x + value);
        }

        public void SetX(float value)
        {
            this.value.x = value;

            if (this.value.x <= 0)
            {
                OnXSetLessThanOrEqualToZero?.Invoke();
            }

            if (this.value.x >= this.value.y)
            {
                OnXSetGreaterThanOrEqualToY?.Invoke();
            }
        }

        [ContextMenu("Set Y to Zero")]
        public void SetYToZero()
        {
            SetY(0);
        }

        public void SetY(float value)
        {
            this.value.y = value;
        }
    }
}
