using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public class MonoBehaviourEnabledThreshold : BaseEnabledThreshold
    {
        public MonoBehaviour obj;

        void Update()
        {
            obj.enabled = IsEnabled();
        }
    }
}

