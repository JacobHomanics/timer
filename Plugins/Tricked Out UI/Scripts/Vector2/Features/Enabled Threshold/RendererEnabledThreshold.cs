using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public class RendererEnabledThreshold : BaseEnabledThreshold
    {
        public new Renderer renderer;

        void Update()
        {
            renderer.enabled = IsEnabled();
        }

        void Reset()
        {
            if (!renderer)
                renderer = GetComponent<Renderer>();
        }
    }
}

