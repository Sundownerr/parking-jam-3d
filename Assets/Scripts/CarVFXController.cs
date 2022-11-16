using UnityEngine;

namespace Game
{
    public class CarVFXController : MonoBehaviour
    {
        [SerializeField] private CollisionListener _collisionListener;
        [SerializeField] private ParticleSystem _backwardCollision;
        [SerializeField] private ParticleSystem _forwardCollision;

        private void Start()
        {
            _collisionListener.CollidedBackward += OnCollidedBackward;
            _collisionListener.CollidedForward += OnCollidedForward;
        }

        private void OnCollidedForward()
        {
            _forwardCollision.Play(true);
        }

        private void OnCollidedBackward()
        {
            _backwardCollision.Play(true);
        }
    }
}