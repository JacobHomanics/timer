using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// Serializable class that pairs a color with a threshold value (0.0 to 100.0).
    /// </summary>
    [System.Serializable]
    public class ColorStop
    {
        public Color color;
        [Range(0f, 100f)]
        public float threshold;

        public ColorStop(Color color, float threshold)
        {
            this.color = color;
            this.threshold = threshold;
        }
    }

    /// <summary>
    /// MonoBehaviour component that applies a color gradient to an image based on current and max values.
    /// Supports an arbitrary list of colors and thresholds.
    /// </summary>
    public abstract class ColorGradientFeatureComponent : BaseCurrentMaxComponent
    {

        [Tooltip("List of color stops defining the gradient. Thresholds should be between 0.0 and 100.0, and will be automatically sorted.")]
        public List<ColorStop> colorStops = new()
        {
            new(Color.red, 0f),
            new(Color.yellow, 50f),
            new(Color.green, 100f)
        };

        protected Color HandleColor()
        {
            return CalculateColor(colorStops, Current, Max);
        }

        public Color CalculateColor(List<ColorStop> colorStops, float current, float max)
        {
            float healthPercent;

            healthPercent = Normalize(current, max);

            healthPercent = Mathf.Clamp01(healthPercent);

            // Convert healthPercent from 0-1 scale to 0-100 scale to match threshold range
            float healthPercentScaled = healthPercent * 100f;

            Color finalColor;

            // Sort color stops by threshold to ensure proper interpolation
            var sortedStops = colorStops.OrderBy(stop => stop.threshold).ToList();

            // Handle edge cases
            if (sortedStops.Count == 1)
            {
                finalColor = sortedStops[0].color;
                return finalColor;
            }

            // Handle cases where healthPercent is outside the range of thresholds
            if (healthPercentScaled <= sortedStops[0].threshold)
            {
                finalColor = sortedStops[0].color;
                return finalColor;
            }

            if (healthPercentScaled >= sortedStops[sortedStops.Count - 1].threshold)
            {
                finalColor = sortedStops[sortedStops.Count - 1].color;
                return finalColor;
            }

            // Find the two color stops to interpolate between
            ColorStop lowerStop = sortedStops[0];
            ColorStop upperStop = sortedStops[sortedStops.Count - 1];

            for (int i = 0; i < sortedStops.Count - 1; i++)
            {
                if (healthPercentScaled >= sortedStops[i].threshold && healthPercentScaled <= sortedStops[i + 1].threshold)
                {
                    lowerStop = sortedStops[i];
                    upperStop = sortedStops[i + 1];
                    break;
                }
            }

            // Interpolate between the two color stops
            float range = upperStop.threshold - lowerStop.threshold;
            if (range <= 0f)
            {
                finalColor = lowerStop.color;
                return finalColor;
            }

            float t = (healthPercentScaled - lowerStop.threshold) / range;
            finalColor = Color.Lerp(lowerStop.color, upperStop.color, t);

            return finalColor;
        }

        public float Normalize(float value1, float value2)
        {
            return value1 / value2;
        }
    }
}

