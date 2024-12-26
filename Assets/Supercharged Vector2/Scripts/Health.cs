using UnityEngine;
using UnityEngine.Events;

namespace JacobHomanics.Core.SuperchargedVector2
{
    [RequireComponent(typeof(Vector2))]
    public class Health : MonoBehaviour
    {
        public bool clampHealthToMax = true;

        void Reset()
        {
            vector2 = GetComponent<Vector2>();
        }

        public Vector2 vector2;

        public float Current
        {
            get => vector2.X;
            set
            {
                var clampedValue = Mathf.Clamp(value, 0, Max);
                vector2.X = clampedValue;
            }
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
        public UnityEvent OnHealthSetToMax;
        public UnityEvent OnRestore;

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

        private void HandleXSetToY()
        {
            OnHealthSetToMax?.Invoke();
        }

        void OnEnable()
        {
            vector2.OnXIncrease.AddListener(HandleXIncrease);
            vector2.OnXDecrease.AddListener(HandleXDecrease);
            vector2.OnXSetLessThanOrEqualToZero.AddListener(HandleXSetLessThanOrEqualToZero);
            vector2.OnXSetToY.AddListener(HandleXSetToY);
        }

        void OnDisable()
        {
            vector2.OnXIncrease.RemoveListener(HandleXIncrease);
            vector2.OnXDecrease.RemoveListener(HandleXDecrease);
            vector2.OnXSetLessThanOrEqualToZero.RemoveListener(HandleXSetLessThanOrEqualToZero);
            vector2.OnXSetToY.RemoveListener(HandleXSetToY);
        }

        public void Decrease(float amount)
        {
            Current -= amount;
        }

        public void Increase(float amount)
        {
            Current += amount;
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

        [ContextMenu("-- Current Health --")]
        private void CurrentHealthHeader() { }

        [ContextMenu("Restore")]
        public void Restore()
        {
            Current = Max;
            OnRestore?.Invoke();
        }

        [ContextMenu("Damage 1")]
        private void Take1Damage()
        {
            Current -= 1;
        }

        [ContextMenu("Damage 10")]
        private void Take10Damage()
        {
            Current -= 10;
        }

        [ContextMenu("Heal 1")]
        private void Heal1()
        {
            Current += 1;
        }

        [ContextMenu("Heal10")]
        private void Heal10()
        {
            Current += 10;
        }
    }
}
