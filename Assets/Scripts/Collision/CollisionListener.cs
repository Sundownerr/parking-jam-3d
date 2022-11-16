using System;
using UnityEngine;

namespace Game
{
    public class CollisionListener : MonoBehaviour
    {
        private bool isActive = true;

        public void Disable()
        {
            isActive = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!isActive)
            {
                return;
            }
            
            var collisionPont = other.bounds.ClosestPoint(transform.position);
            var collisionDirection = collisionPont - transform.position;

            var forward = transform.forward.normalized;
            var dot = Vector3.Dot(forward, collisionDirection.normalized);
            var collisionEvent = new CollisionEvent(collisionDirection.normalized, collisionPont, dot, other.gameObject);
            
            if (other.gameObject.TryGetComponent<Obstacle>(out var obstacle))
            {
                obstacle.HandleCollision();
            }
            

            Collided?.Invoke(collisionEvent);
        }

        public event Action<CollisionEvent> Collided;
    }
}