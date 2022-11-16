using UnityEngine;

namespace Game
{
    public class CarVFXController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _collisionVFX;
        [SerializeField] private ParticleSystem _moveVFX;

        public void PlayCollisionVFX(CollisionEvent e)
        {
            _collisionVFX.transform.position = e.Point;
            _collisionVFX.Play(true);
        }

        public void PlayMoveVfx()
        {
            _moveVFX.Play(true);
        }
    }
}