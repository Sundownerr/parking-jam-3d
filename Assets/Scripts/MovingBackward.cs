using System;
using UnityEngine;

namespace Game
{
    public class MovingBackward : Moving
    {
        public MovingBackward(Transform transform, float moveSpeed) : base(transform, moveSpeed)
        { }

        protected override Vector3 GetDirection()
        {
            return -_transform.forward;
        }
    }
}