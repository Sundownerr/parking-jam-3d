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
            
            var collisionDirection = other.transform.position - transform.position;
            var collisionPont = other.bounds.ClosestPoint(transform.position);
            var forward = transform.TransformDirection(Vector3.forward);
            var dot = Vector3.Dot(forward.normalized, collisionDirection.normalized);
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