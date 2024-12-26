using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.Core.Timer
{
    [RequireComponent(typeof(Vector2))]
    public class Health : MonoBehaviour
    {
        void Reset()
        {
            vector2 = GetComponent<Vector2>();
        }

        public Vector2 vector2;

        public float Current
        {
            get => vector2.X;
            set => vector2.X = value;
        }

        public float Max
        {
            get => vector2.Y;
            set => vector2.Y = value;
        }


        [Header("Events")]
        public UnityEvent OnHealthIncrease;
        public UnityEvent OnHealthDecrease;
        public UnityEvent OnDeath;

        private void HandleXDecrease()
        {
            OnHealthDecrease?.Invoke();
        }

        private void HandleXIncrease()
        {
            OnHealthIncrease?.Invoke();
        }

        private void HandleXSetLessThanOrEqualToZero()
        {
            OnDeath?.Invoke();
        }

        void OnEnable()
        {
            vector2.OnXIncrease.AddListener(HandleXIncrease);
            vector2.OnXDecrease.AddListener(HandleXDecrease);
            vector2.OnXSetLessThanOrEqualToZero.AddListener(HandleXSetLessThanOrEqualToZero);
        }

        void OnDisable()
        {
            vector2.OnXIncrease.RemoveListener(HandleXIncrease);
            vector2.OnXDecrease.RemoveListener(HandleXDecrease);
            vector2.OnXSetLessThanOrEqualToZero.RemoveListener(HandleXSetLessThanOrEqualToZero);
        }

        public void TakeDamage(float amount)
        {
            vector2.OffsetX(-amount);
        }

        public bool IsMaxHealth()
        {
            return Current == Max;
        }

        public bool IsMaxHealthOrGreater()
        {
            return Current >= Max;
        }

        public bool IsZero()
        {
            return Current == 0;
        }

        public bool IsZeroOrLess()
        {
            return Current <= 0;
        }
    }
}
