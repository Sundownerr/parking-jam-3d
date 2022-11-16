using UnityEngine;

namespace Game
{
    public class MovingForward : Moving
    {
        public MovingForward(Transform transform, float moveSpeed) : base(transform, moveSpeed)
        { }

        protected override Vector3 GetDirection()
        {
            return _transform.forward;
        }
    }
}