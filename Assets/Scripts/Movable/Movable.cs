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