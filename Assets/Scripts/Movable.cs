using System;
using UnityEngine;

namespace Game
{
    public class Movable : MonoBehaviour
    {
        public event Action MovedForward;
        public event Action MovedBackward;

        public void Move(Vector3 swipeDirection)
        {
            var position = transform.position;
            var forward = transform.forward;

            var forwardAngle = Vector3.Angle(position + forward, swipeDirection);
            var backAngle = Vector3.Angle(position - forward, swipeDirection);

            if (forwardAngle > backAngle)
            {
                MovedBackward?.Invoke();
            }
            else
            {
                MovedForward?.Invoke();
            }
        }
    }
}