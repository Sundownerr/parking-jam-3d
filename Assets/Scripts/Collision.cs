using DG.Tweening;
using UnityEngine;

namespace Game
{
    public abstract class Collision : IState, IEnterState
    {
        private readonly IState _nextState;
        private readonly IStateController _stateController;
        protected readonly Transform _transform;

        protected Collision(Transform transform, IState nextState, IStateController stateController)
        {
            _transform = transform;
            _nextState = nextState;
            _stateController = stateController;
        }

        public void Enter()
        {
            _transform.DOMove(_transform.position + GetDirection(), 0.1f)
                .OnComplete(() => { _stateController.ChangeState(_nextState); });
        }

        protected abstract Vector3 GetDirection();
    }
}