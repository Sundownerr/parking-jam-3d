using System;
using UnityEngine;

namespace Game
{
    public class PlayerInputController : MonoBehaviour
    {
        private bool isSwiped;

        private void Update()
        {
            if (Input.touchCount <= 0)
            {
                return;
            }

            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    if (touch.deltaPosition.magnitude > 1f && !isSwiped)
                    {
                        isSwiped = true;
                        FingerPressed?.Invoke(touch);
                    }
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    isSwiped = false;
                    FingerReleased?.Invoke(touch);
                    break;
                case TouchPhase.Canceled:
                    isSwiped = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public event Action<Touch> FingerPressed;
        public event Action<Touch> FingerReleased;
    }
}