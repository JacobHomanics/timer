using UnityEngine;
using UnityEngine.UI;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that handles animated background fill based on value changes.
    /// </summary>
    public abstract class BaseAnimatedFill : BaseVector2Component
    {
        [System.Serializable]
        public class Properties
        {
            [Tooltip("Duration in seconds for the animation to complete")]
            public float animationDuration = 1f;
            public float delay = 0.5f;
        }

        public Properties properties;

        protected float previousValue;

        protected bool isAnimating = false;
        protected float animationFromValue;
        protected float animationToValue;
        protected float animationElapsed;
        protected float animationDuration;
        protected float animationDelayRemaining;

        public float HandleValueChange(float newValue, float fillAmount, ref float previousValue, float max, float delay, float duration)
        {
            if (Mathf.Abs(newValue - previousValue) < 0.001f)
                return fillAmount;

            // If already animating to the same target, don't restart
            if (isAnimating && Mathf.Abs(animationToValue - newValue) < 0.001f)
            {
                previousValue = newValue;
                return fillAmount;
            }

            // If already animating (including during delay), use the stored starting value
            // to avoid recalculating from fillAmount which might have drifted
            float startValue;
            if (isAnimating)
            {
                startValue = animationFromValue;
            }
            else
            {
                // Get the current background fill value
                float currentFillValue = GetFillValue(fillAmount, max);

                // Check if background fill needs initialization (is at or near 0, indicating uninitialized)
                // Only initialize if it's truly uninitialized, not just different
                if (currentFillValue < 0.01f * max)
                {
                    // Background fill appears uninitialized, initialize it to previousValue
                    currentFillValue = previousValue;
                }

                startValue = currentFillValue;
            }

            // Set up animation state
            StartAnimation(startValue, newValue, max, delay, duration, ref fillAmount);

            previousValue = newValue;
            // Return the exact starting value to prevent any visible change before delay
            // UpdateAnimation will maintain this value during the delay
            return Normalize(animationFromValue, max);
        }
        public void StartAnimation(float fromValue, float toValue, float max, float delay, float duration, ref float fillAmount)
        {
            float valueDifference = Mathf.Abs(fromValue - toValue);
            if (valueDifference < 0.001f)
            {
                fillAmount = Normalize(toValue, max);
                isAnimating = false;
                return;
            }

            // Initialize animation state
            // Even if duration is 0, we still need to set up animation to handle delay
            isAnimating = true;
            animationFromValue = fromValue;
            animationToValue = toValue;
            animationElapsed = 0f;
            animationDelayRemaining = delay;
            animationDuration = Mathf.Max(0f, duration); // Ensure non-negative
        }

        public float UpdateAnimation(float fillAmount, float max)
        {
            if (!isAnimating)
                return fillAmount;

            // Handle delay before animation starts
            // During delay, maintain fillAmount at the exact starting value
            // This ensures no drift occurs during the delay period
            if (animationDelayRemaining > 0f)
            {
                animationDelayRemaining -= Time.deltaTime;
                // Always return the exact starting value during delay
                return Normalize(animationFromValue, max);
            }

            // If duration is 0, complete animation immediately after delay
            if (animationDuration <= 0f)
            {
                fillAmount = Normalize(animationToValue, max);
                isAnimating = false;
                return fillAmount;
            }

            // Update animation
            animationElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(animationElapsed / animationDuration);
            float currentValue = Mathf.Lerp(animationFromValue, animationToValue, t);
            fillAmount = Normalize(currentValue, max);


            // Check if animation is complete
            if (animationElapsed >= animationDuration)
            {
                fillAmount = Normalize(animationToValue, max);
                isAnimating = false;
            }

            return fillAmount;
        }

        public float Normalize(float amount, float max)
        {
            return amount / max;
        }

        public static float GetFillValue(float fillAmount, float max)
        {
            return fillAmount * max;
        }
    }
}

