using UnityEngine;

namespace JacobHomanics.TrickedOutUI
{
    /// <summary>
    /// MonoBehaviour component that creates a flashing effect when a value falls below a threshold.
    /// </summary>
    public class GameObjectEnabledThreshold : BaseEnabledThreshold
    {
        public GameObject obj;

        void Update()
        {
            obj.SetActive(IsEnabled());
        }

        void Reset()
        {
            if (!obj)
                obj = this.gameObject;
        }
    }
}

