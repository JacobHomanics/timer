using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// Serializable class that pairs a color with a threshold value (0.0 to 1.0).
    /// </summary>
    [System.Serializable]
    public class ColorStop
    {
        public Color color;
        [Range(0f, 1f)]
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
    public class ColorGradientFeatureComponent : BaseCurrentMaxComponent
    {
        public Image image;

        [Tooltip("List of color stops defining the gradient. Thresholds should be between 0.0 and 1.0, and will be automatically sorted.")]
        public List<ColorStop> colorStops = new List<ColorStop>
        {
            new ColorStop(Color.red, 0f),
            new ColorStop(Color.yellow, 0.5f),
            new ColorStop(Color.green, 1f)
        };

        void Update()
        {
            ColorGradientFeatureCommand(colorStops, image, Current, Max);
        }

        public static void ColorGradientFeatureCommand(List<ColorStop> colorStops, Image image, float current, float max)
        {
            float healthPercent;

            healthPercent = current / max;

            healthPercent = Mathf.Clamp01(healthPercent);

            // Sort color stops by threshold to ensure proper interpolation
            var sortedStops = colorStops.OrderBy(stop => stop.threshold).ToList();

            // Handle edge cases
            if (sortedStops.Count == 1)
            {
                image.color = sortedStops[0].color;
                return;
            }

            // Handle cases where healthPercent is outside the range of thresholds
            if (healthPercent <= sortedStops[0].threshold)
            {
                image.color = sortedStops[0].color;
                return;
            }

            if (healthPercent >= sortedStops[sortedStops.Count - 1].threshold)
            {
                image.color = sortedStops[sortedStops.Count - 1].color;
                return;
            }

            // Find the two color stops to interpolate between
            ColorStop lowerStop = sortedStops[0];
            ColorStop upperStop = sortedStops[sortedStops.Count - 1];

            for (int i = 0; i < sortedStops.Count - 1; i++)
            {
                if (healthPercent >= sortedStops[i].threshold && healthPercent <= sortedStops[i + 1].threshold)
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
                image.color = lowerStop.color;
                return;
            }

            float t = (healthPercent - lowerStop.threshold) / range;
            image.color = Color.Lerp(lowerStop.color, upperStop.color, t);
        }
    }
}

