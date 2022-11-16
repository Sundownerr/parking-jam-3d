using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private CollisionListener _collisionListener;
        [SerializeField] private CarCollisionHandler _carCollisionHandler;
        [SerializeField] private CarVFXController _carVFXController;
        [SerializeField] private CarSFXController _carSfxController;
        [SerializeField] private CinemachineDollyCart _cart;
        [SerializeField] private Movable _movable;

        private void Start()
        {
            _collisionListener.Collided += OnCollided;
            _movable.MovedForward += OnMovedForward;
            _movable.MovedBackward += OnMovedBackward;

            _movable.StopMoving();
            _cart.enabled = false;
        }

        private void OnDestroy()
        {
            _collisionListener.Collided -= OnCollided;
            _movable.MovedForward -= OnMovedForward;
            _movable.MovedBackward -= OnMovedBackward;
        }

        private void OnMovedBackward()
        {
            Move(-transform.forward);
        }

        private void Move(Vector3 direction)
        {
            _carSfxController.StartEngineSFX();
            _carVFXController.PlayMoveVfx();

            DOTween.Kill(transform, true);

            var rotation = transform.InverseTransformDirection(direction);
            (rotation.x, rotation.z) = (-rotation.z, rotation.x);

            transform.DOPunchRotation(rotation * 3f, 0.4f, 8).SetId(transform);

            direction.y = 0;

            _movable.SetDirection(direction);
            _movable.StartMoving();
        }

        private void OnMovedForward()
        {
            Move(transform.forward);
        }

        private void OnCollided(CollisionEvent e)
        {
            _movable.StopMoving();

            if (e.GameObject.TryGetComponent<FinishPathTrigger>(out var finish))
            {
                _collisionListener.Disable();

                var path = finish.Path;
                _cart.m_Path = path;

                var closestPoint = path.FindClosestPoint(e.Point, 0, -1, 10);
                closestPoint = path.FromPathNativeUnits(closestPoint, _cart.m_PositionUnits);

                var startPosition = path.EvaluatePositionAtUnit(closestPoint, _cart.m_PositionUnits);
                var startRotation = path.EvaluateOrientationAtUnit(closestPoint, _cart.m_PositionUnits);

                transform.DOMove(startPosition, 0.2f);

                transform.DORotateQuaternion(startRotation, 0.2f)
                    .OnComplete(() =>
                    {
                        _cart.m_Position = closestPoint;
                        _cart.enabled = true;

                        DOVirtual.Float(_cart.m_Speed / 3f, _cart.m_Speed, 0.2f, value => _cart.m_Speed = value);
                    });

                return;
            }

            if (e.GameObject.TryGetComponent<Car>(out var otherCar))
            {
                if (CarCollisionHandler.CheckIsHitFromSides(e.DotProduct))
                {
                    _carSfxController.PlayOtherCarCollisionReaction();
                }
            }

            _carSfxController.StopEngineSFX();
            _carSfxController.PlayCollisionSFX();
            _carCollisionHandler.HandleCollision(e);
            _carVFXController.PlayCollisionVFX(e);
        }
    }
}