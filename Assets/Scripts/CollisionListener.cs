using System;
using UnityEngine;

namespace Game
{
    public class CollisionListener : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Obstacle>(out var obstacle))
            {
                obstacle.HandleCollision();
            }

            if (other.gameObject.TryGetComponent<Car>(out var car))
            {
                // car.HandleCollision();
            }

            
            var collisionDirection = other.transform.position - transform.position;
            var collisionPont = other.bounds.ClosestPoint(transform.position);
            var forward = transform.TransformDirection(Vector3.forward);
            var dot = Vector3.Dot(forward.normalized, collisionDirection.normalized);

            Collided?.Invoke(new CollisionEvent(collisionDirection.normalized, collisionPont, dot));
            // Debug.Log(dot);
            if (dot < 0)
            {
                CollidedBackward?.Invoke();
            }
            else
            {
                CollidedForward?.Invoke();
            }
        }

        public event Action CollidedForward;
        public event Action CollidedBackward;
        public event Action<CollisionEvent> Collided;
    }
}