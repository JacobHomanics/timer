using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JacobHomanics.TimerSystem
{
    public class TimerImage : MonoBehaviour
    {
        [Header("References")]
        public Timer timer;
        public Image image;
        public bool reverseFill;

        void Update()
        {
            if (reverseFill)
                image.fillAmount = (timer.Duration - timer.ElapsedTime) / timer.Duration;
            else
                image.fillAmount = timer.ElapsedTime / timer.Duration;
        }
    }
}