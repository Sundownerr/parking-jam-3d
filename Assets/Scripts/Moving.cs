using UnityEngine;

namespace Game
{
    public abstract class Moving : IUpdateState, IEnterState, IExitState
    {
        private readonly float _moveSpeed;
        protected readonly Transform _transform;
        private float _speed;

        protected Moving(Transform transform, float moveSpeed)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
        }

        public void Update()
        {
            _speed = Mathf.Lerp(_speed, _moveSpeed, Time.deltaTime * 5f);
            _transform.position += GetDirection() * (Time.deltaTime * _speed);
        }

        protected abstract Vector3 GetDirection();
        public void Enter()
        {
            _speed = 0;
        }

        public void Exit()
        {
            _speed = 0;
        }
    }
}