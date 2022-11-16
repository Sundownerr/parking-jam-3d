using UnityEngine;

namespace Game
{
    public struct CollisionEvent
    {
        public Vector3 Direction;
        public Vector3 Point;
        public float DotProduct;

        public CollisionEvent(Vector3 direction, Vector3 point, float dotProduct)
        {
            Direction = direction;
            Point = point;
            DotProduct = dotProduct;
        }
    }
}