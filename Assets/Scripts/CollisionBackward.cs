using UnityEngine;

namespace Game
{
    public class CollisionBackward : Collision
    {
        public CollisionBackward(Transform transform, IState nextState, IStateController stateController) : base(
            transform, nextState, stateController)
        { }

        protected override Vector3 GetDirection()
        {
            return _transform.forward * 0.1f;
        }
    }
}