using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.Timer
{
    public abstract class ColorBase : MonoBehaviour
    {
        [Header("Configuration")]
        public Color colorOnDurationReached = Color.white;
        private Color originalColor;

        [Header("References")]
        public Timer timer;

        protected abstract Color GetOriginalColor();

        void Start()
        {
            originalColor = GetOriginalColor();
        }

        protected void SetColor(out Color color, Timer timer)
        {
            if (timer.IsDurationReached())
                color = colorOnDurationReached;
            else
                color = originalColor;
        }

    }
}