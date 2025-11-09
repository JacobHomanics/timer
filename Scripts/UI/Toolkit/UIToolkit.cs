using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public abstract class FeatureToggle
{
}

[System.Serializable]
public class TextDisplayFeature2 : FeatureToggle
{
    public TMP_Text text;

    public enum DisplayType
    {
        Current, Max, Difference
    }

    public DisplayType displayType;
    public string format = "#,##0";

    public TextDisplayFeature2()
    {
    }

    public TextDisplayFeature2(DisplayType displayType)
    {
        this.displayType = displayType;
    }
}

[System.Serializable]
public class TextDisplayFeature : FeatureToggle
{
    public TextDisplayFeature2 textCurrent = new(TextDisplayFeature2.DisplayType.Current);
    public TextDisplayFeature2 textMax = new(TextDisplayFeature2.DisplayType.Max);
}

[System.Serializable]
public class ColorGradientFeature : FeatureToggle
{
    public Color colorAtMin = Color.red;
    public Color colorAtHalfway = Color.yellow;
    public Color colorAtMax = Color.green;
    public bool reverseFill;
}

[System.Serializable]
public class BackgroundFillFeature : FeatureToggle
{
    public Image backgroundFill;
    public bool keepSizeConsistent = true;
    public float animationSpeed = 10;
    public AnimationCurve speedCurve = AnimationCurve.EaseInOut(0f, 0.3f, 1f, 16f);
    public float delay = 1f;

    public bool reverseFill;

    public bool isAnimating = false;
    public float animationFromValue;
    public float animationToValue;
    public float animationElapsed;
    public float animationDuration;
    public float animationDelayRemaining;


}

[System.Serializable]
public class FlashingFeature : FeatureToggle
{
    public Image flashImage;
    public float thresholdPercent = 0.2f;
    public Color flashColor1 = Color.red;
    public Color flashColor2 = Color.white;
    public float flashSpeed = 15f;

    public bool reverseFill;
}

public static class UIToolkit
{
    public static T GetFeature<T>(List<FeatureToggle> featureToggles) where T : FeatureToggle
    {
        return featureToggles.Find(f => f is T) as T;
    }

    public static void Display(TextDisplayFeature2 textDisplayFeature, float current, float max)
    {
        if (textDisplayFeature.displayType == TextDisplayFeature2.DisplayType.Current)
            Display(textDisplayFeature.text, current, textDisplayFeature.format);
        if (textDisplayFeature.displayType == TextDisplayFeature2.DisplayType.Max)
            Display(textDisplayFeature.text, max, textDisplayFeature.format);
        if (textDisplayFeature.displayType == TextDisplayFeature2.DisplayType.Difference)
            Display(textDisplayFeature.text, max - current, textDisplayFeature.format);
    }

    public static void Display(TMP_Text text, float num, string format)
    {
        text.text = num.ToString(format);
    }

    // Flashing Fill Rect Feature
    public static void FlashingFeatureCommand(FlashingFeature flashingFeature, float current, float max)
    {
        float healthPercent = current / max;

        if (flashingFeature.reverseFill)
            healthPercent = (max - current) / max;
        else
            healthPercent = current / max;


        healthPercent = Mathf.Clamp01(healthPercent);

        flashingFeature.flashImage.gameObject.SetActive(healthPercent < flashingFeature.thresholdPercent);
        if (flashingFeature != null && healthPercent < flashingFeature.thresholdPercent)
        {
            // Calculate flashing based on time
            float flashValue = Mathf.Sin(Time.time * flashingFeature.flashSpeed) * 0.5f + 0.5f;
            Color flashColor = Color.Lerp(flashingFeature.flashColor1, flashingFeature.flashColor2, flashValue);
            flashingFeature.flashImage.color = flashColor;
            flashingFeature.flashImage.fillAmount = healthPercent;
        }
    }

    public static void ColorGradientFeatureCommand(ColorGradientFeature colorFeature, Image image, float current, float max)
    {
        float healthPercent = current / max;

        if (colorFeature.reverseFill)
            healthPercent = (max - current) / max;
        else
            healthPercent = current / max;


        if (colorFeature != null)
        {
            Color healthColor;
            if (healthPercent > 0.5f)
            {
                // Green to yellow
                float t = (healthPercent - 0.5f) * 2f;
                healthColor = Color.Lerp(colorFeature.colorAtHalfway, colorFeature.colorAtMax, t);
            }
            else
            {
                // Yellow to red
                float t = healthPercent * 2f;
                healthColor = Color.Lerp(colorFeature.colorAtMin, colorFeature.colorAtHalfway, t);
            }

            image.color = healthColor;
        }
    }

    public static void HandleValueChange(float newValue, BackgroundFillFeature bgFeature, ref float previousValue, float max)
    {
        if (Mathf.Abs(newValue - previousValue) < 0.001f)
            return;

        if (bgFeature == null || bgFeature.backgroundFill == null)
        {
            previousValue = newValue;
            return;
        }

        var reverseFill = bgFeature.reverseFill;

        // Get the current background fill value
        float currentFillValue = UIToolkit.GetBackgroundFillValue(bgFeature, max);

        // Check if background fill needs initialization (is at or near 0, indicating uninitialized)
        // Only initialize if it's truly uninitialized, not just different
        if (currentFillValue < 0.01f * max)
        {
            // Background fill appears uninitialized, initialize it to previousValue
            UIToolkit.SetBackgroundFillAmount(bgFeature, previousValue, max, reverseFill);
            currentFillValue = previousValue;
        }

        // Get the starting value based on whether we want to keep size consistent
        float startValue;
        if (bgFeature.keepSizeConsistent)
        {
            // Use current background fill position (continues from where it is)
            startValue = currentFillValue;
        }
        else
        {
            // Reset to previous value (starts from previous slider value)
            startValue = previousValue;
            UIToolkit.SetBackgroundFillAmount(bgFeature, previousValue, max, reverseFill);
        }

        // If new value is greater than start position, immediately snap to it
        if (newValue > startValue)
        {
            // Stop any ongoing animation
            bgFeature.isAnimating = false;
            // Immediately set to new value
            UIToolkit.SetBackgroundFillAmount(bgFeature, newValue, max, reverseFill);
        }
        else
        {
            // HP goes down or stays same - animate from start position
            // Set up animation state
            UIToolkit.StartBackgroundFillAnimation(startValue, newValue, bgFeature, max, reverseFill);
        }

        previousValue = newValue;
    }

    public static void StartBackgroundFillAnimation(float fromValue, float toValue, BackgroundFillFeature bgFeature, float max, bool reverseFill)
    {
        float valueDifference = Mathf.Abs(fromValue - toValue);
        if (valueDifference < 0.001f)
        {
            SetBackgroundFillAmount(bgFeature, toValue, max, reverseFill);
            bgFeature.isAnimating = false;
            return;
        }

        // Initialize animation state
        bgFeature.isAnimating = true;
        bgFeature.animationFromValue = fromValue;
        bgFeature.animationToValue = toValue;
        bgFeature.animationElapsed = 0f;
        bgFeature.animationDelayRemaining = bgFeature.delay;

        // Calculate dynamic animation speed based on difference
        float normalizedDifference = valueDifference / max;
        float speedMultiplier = bgFeature.speedCurve.Evaluate(normalizedDifference);
        float dynamicSpeed = bgFeature.animationSpeed * speedMultiplier;
        bgFeature.animationDuration = valueDifference / dynamicSpeed;
    }

    public static void UpdateBackgroundFillAnimation(BackgroundFillFeature bgFeature, float max)
    {
        if (!bgFeature.isAnimating)
            return;

        // Handle delay before animation starts
        if (bgFeature.animationDelayRemaining > 0f)
        {
            bgFeature.animationDelayRemaining -= Time.deltaTime;
            return;
        }

        // Update animation
        bgFeature.animationElapsed += Time.deltaTime;
        float t = Mathf.Clamp01(bgFeature.animationElapsed / bgFeature.animationDuration);
        float currentValue = Mathf.Lerp(bgFeature.animationFromValue, bgFeature.animationToValue, t);
        SetBackgroundFillAmount(bgFeature, currentValue, max, bgFeature.reverseFill);

        // Check if animation is complete
        if (bgFeature.animationElapsed >= bgFeature.animationDuration)
        {
            SetBackgroundFillAmount(bgFeature, bgFeature.animationToValue, max, bgFeature.reverseFill);
            bgFeature.isAnimating = false;
        }
    }

    public static void SetBackgroundFillAmount(BackgroundFillFeature bgFeature, float amount, float max, bool reverseFill)
    {
        if (bgFeature != null && bgFeature.backgroundFill != null)
        {
            if (reverseFill)
                bgFeature.backgroundFill.fillAmount = (max - amount) / max;
            else
                bgFeature.backgroundFill.fillAmount = amount / max;

            // bgFeature.backgroundFill.fillAmount = amount / max;
        }
    }

    public static float GetBackgroundFillValue(BackgroundFillFeature bgFeature, float max)
    {
        return bgFeature.backgroundFill.fillAmount * max;
    }
}
