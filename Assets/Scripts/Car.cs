using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Car : MonoBehaviour, IStateController
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private CollisionListener _collisionListener;
        [SerializeField] private Movable _movable;

        private CollisionBackward _collisionBackward;
        private CollisionForward _collisionForward;
        private Idle _idle;
        private MovingBackward _movingBackward;
        private MovingForward _movingForward;
        private IState _state;

        private void Start()
        {
            _idle = new Idle();
            _movingBackward = new MovingBackward(transform, _moveSpeed);
            _movingForward = new MovingForward(transform, _moveSpeed);
            _collisionForward = new CollisionForward(transform, _idle, this);
            _collisionBackward = new CollisionBackward(transform, _idle, this);

            _collisionListener.Collided += CollisionListenerOnCollided;
            _movable.MovedForward += OnMovedForward;
            _movable.MovedBackward += OnMovedBackward;

            ChangeState(_idle);
        }

        private void Update()
        {
            if (_state is IUpdateState updateState)
            {
                updateState.Update();
            }
        }

        private void OnDestroy()
        {
            _movable.MovedForward -= OnMovedForward;
            _movable.MovedBackward -= OnMovedBackward;
        }

        public void ChangeState(IState nextState)
        {
            if (_state is IExitState exitState)
            {
                exitState.Exit();
            }

            _state = nextState;

            if (_state is IEnterState enterState)
            {
                enterState.Enter();
            }
        }

        private void CollisionListenerOnCollided(CollisionEvent e)
        {
            ChangeState(_idle);
            DOTween.Kill(transform, true);

            var rotation = transform.InverseTransformDirection(e.Direction);
            (rotation.x, rotation.z) = (-rotation.z, rotation.x);

            transform.DOPunchRotation(rotation * 7f, 0.3f, 8).SetId(transform);


            var isHitFromSide = Mathf.Abs(e.DotProduct) < 0.5f;

            if (isHitFromSide)
            {
                return;
            }
            
            var moveDireciton = transform.forward * -e.DotProduct;
            transform.DOLocalMove(transform.localPosition + moveDireciton * 0.3f, 0.2f).SetId(transform);
        }

        private void OnMovedBackward()
        {
            if (_state != _idle)
            {
                return;
            }

            ChangeState(_movingBackward);
        }

        private void OnMovedForward()
        {
            if (_state != _idle)
            {
                return;
            }

            ChangeState(_movingForward);
        }
    }
}