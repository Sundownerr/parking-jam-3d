using System;
using UnityEngine;

namespace Game
{
    public class Movable : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _acceleration;
        private Vector3 _moveDirection;
        private float _speed;

        public void Update()
        {
            _speed = Mathf.Lerp(_speed, _maxSpeed, Time.deltaTime * _acceleration);
            transform.position += _moveDirection * (Time.deltaTime * _speed);
        }

        public event Action MovedForward;
        public event Action MovedBackward;

        public void HandleSwipe(Vector3 swipeDirection)
        {
            var forward = transform.TransformDirection(Vector3.forward);
            var dot = Vector3.Dot(forward.normalized, swipeDirection.normalized);

            if (dot < 0)
            {
                MovedBackward?.Invoke();
            }
            else
            {
                MovedForward?.Invoke();
            }
        }

        public void StartMoving()
        {
            enabled = true;
        }

        public void SetDirection(Vector3 direction)
        {
            _moveDirection = direction;
        }

        public void StopMoving()
        {
            _speed = 0;
            enabled = false;
        }
    }
}