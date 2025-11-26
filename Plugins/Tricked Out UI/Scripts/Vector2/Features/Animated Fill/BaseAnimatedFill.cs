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
            [Tooltip("Duration in seconds for the animation to complete when value increases")]
            public float animationDurationUp = 1f;
            [Tooltip("Delay in seconds before animation starts when value increases")]
            public float delayUp = 0.5f;
            [Tooltip("Duration in seconds for the animation to complete when value decreases")]
            public float animationDurationDown = 1f;

            [Tooltip("Delay in seconds before animation starts when value decreases")]
            public float delayDown = 0.5f;
        }

        public Properties properties;

        protected float previousValue;

        protected bool isAnimating = false;
        protected float animationFromValue;
        public float animationToValue { get; set; }
        protected float animationElapsed;
        protected float animationDuration;
        public float animationDelayRemaining { get; set; }

        public float HandleValueChange(float newValue, float fillAmount, ref float previousValue, float max)
        {
            if (Mathf.Abs(newValue - previousValue) < 0.001f)
                return fillAmount;

            // If already animating to the same target, don't restart
            if (isAnimating && Mathf.Abs(animationToValue - newValue) < 0.001f)
            {
                previousValue = newValue;
                return fillAmount;
            }

            // If already animating, calculate the current animated value to continue from there
            float startValue;
            if (isAnimating)
            {
                // Calculate current value based on animation progress
                if (animationDelayRemaining > 0f)
                {
                    // Still in delay, use the starting value
                    startValue = animationFromValue;
                }
                else if (animationDuration > 0f)
                {
                    // Calculate current interpolated value
                    float t = Mathf.Clamp01(animationElapsed / animationDuration);
                    startValue = Mathf.Lerp(animationFromValue, animationToValue, t);
                }
                else
                {
                    // Duration was 0, should be at target
                    startValue = animationToValue;
                }
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

            // Determine direction and select appropriate duration and delay
            bool isGoingUp = newValue > startValue;
            float duration = isGoingUp ? properties.animationDurationUp : properties.animationDurationDown;
            float delay = isGoingUp ? properties.delayUp : properties.delayDown;

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

        /// <summary>
        /// Immediately sets the fill amount to the specified value, bypassing any animation or delay.
        /// Stops any ongoing animation and updates the previous value.
        /// </summary>
        /// <param name="value">The target value to set</param>
        /// <param name="max">The maximum value used for normalization</param>
        /// <returns>The normalized fill amount (value / max)</returns>
        public float SetFillImmediate(float value, float max)
        {
            // Stop any ongoing animation
            isAnimating = false;
            animationDelayRemaining = 0f;
            animationElapsed = 0f;

            // Update previous value to match the new value
            previousValue = value;

            // Return the normalized fill amount
            return Normalize(value, max);
        }
    }
}

